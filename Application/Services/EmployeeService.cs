using System;
using Application.helper;
using Application.Interfaces;
using Application.Shared;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly Cryptographic _encrypt;
        private readonly UploadFile _upload;
        private readonly UserCreator _Creator;

        public EmployeeService(IServiceFactory service, IMapper mapper, UploadFile uploadFile)
        {
            _service = service;
            _encrypt = new Cryptographic();
            _mapper = mapper;
            _upload = uploadFile;
            _Creator = new UserCreator(service);
        }


        public async Task<ResponseMessage> DeleteEmployee(int id)
        {
            try
            {
                if (await _service.GetService<Employee>().GetByIdAsync(id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<Employee>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

        public async Task<PaginationResponse<EmployeeResponse>> GetAllEmployee(string url , int pageSize, int currentPage, int BranchId, int RoleId, string search, string status)
        {
            var all = (await _service.GetService<Employee>()
                         .GetAllWithIncludeAsync(e => e.Branch, e => e.Role)).Select(e => new EmployeeResponse
                         {
                             EmployeeId = e.EmployeeId,
                             BranchId = e.BranchId,
                             RoleId = e.RoleId,
                             Fullname = e.Fullname,
                             Email = e.Email,
                             PhoneNumber = e.PhoneNumber,
                             PasswordHash = e.PasswordHash,
                             HireDate = e.HireDate,
                             Status = e.Status,
                             CreatedDate = e.CreatedDate,
                             CreatedBy = e.CreatedBy,
                             IsUsed = e.IsUsed,
                             Image = _upload.CombineUrlPath(url ,e.Image),
                             Branch = new BranchShortResponse
                             {
                                 BranchId = e.Branch.BranchId,
                                 Name = e.Branch.BranchName
                             },
                             Role = new RoleShortResponse
                             {
                                 RoleId = e.Role.RoleId,
                                 Name = e.Role.RoleName
                             }
                         }).ToList();

            if (BranchId != 0)
            {
                all = all.Where(e => e.BranchId == BranchId).ToList();
            }
            if (RoleId != 0)
            {
                all = all.Where(e => e.RoleId == RoleId).ToList();

            }
            if (!string.IsNullOrEmpty(search))
            {
                all = all.Where(e => e.Email.Contains(search) || e.Fullname.Contains(search)).ToList();

            }
            if (!string.IsNullOrEmpty(status))
            {
                all = all.Where(e => e.Status == status).ToList();
            }

            foreach (var item in all) {
                item.CreatedBy = _Creator.getCreatorEmployee(int.Parse(item.CreatedBy));
                item.PasswordHash = _encrypt.Decrypt(item.PasswordHash);
            };


            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<EmployeeResponse>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<ResponseData> GetEmployeeById(string url, int id)
        {
            var result = _mapper.Map<Employee, EmployeeResponse>(await _service.GetService<Employee>().GetByIdAsync(id));
            result.CreatedBy = _Creator.getCreatorEmployee(int.Parse(result.CreatedBy));
            result.PasswordHash = _encrypt.Decrypt(result.PasswordHash);
            result.Image = _upload.CombineUrlPath(url, result.Image);
            if (result == null) return new ResponseData(404, false, "Data is NotFound");
            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseMessage> SaveEmployee(EmployeeRequest req)
        {
            if (await _service.GetService<Role>().GetByIdAsync(req.RoleId) == null ||
                await _service.GetService<Branch>().GetByIdAsync(req.BranchId) == null)
            {
                return new ResponseMessage(404, true, "Data is NotFound");
            }
            try
            {
                var result = _mapper.Map<EmployeeRequest, Employee>(req);
                if (req.EmployeeId == 0)
                {
                    if (_service.GetService<Employee>().GetAll().Where(e => e.Email == req.Email).FirstOrDefault() != null)
                    {
                        return new ResponseMessage(400, false, "This email address is already in use.");
                    }
                    result.BranchId = req.BranchId == 0 ? null : req.BranchId;
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    result.PasswordHash = _encrypt.Encrypt(req.PasswordHash);
                    if (req.ImageFile != null && req.ImageFile is IFormFile imageFile)
                    {
                        var pathResult = await _upload.GetPathFile(2, imageFile, "employee");
                        result.Image = pathResult.FilePath ?? "";
                    }
                    else
                    {
                        result.Image = "NoImage.png";
                    }
                    await _service.GetService<Employee>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<Employee>().GetByIdAsync(req.EmployeeId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    if (result.Email != req.Email)
                    {
                        if (_service.GetService<Employee>().GetAll().Where(e => e.Email == req.Email).FirstOrDefault() != null)
                        {
                            return new ResponseMessage(400, false, "This email address is already in use.");
                        }
                    }
                    result.BranchId = req.BranchId;
                    result.RoleId = req.RoleId;
                    result.Fullname = req.Fullname;
                    result.PhoneNumber = req.PhoneNumber;
                    result.PasswordHash = req.PasswordHash;
                    result.HireDate = req.HireDate;
                    result.Status = req.Status; 
                    result.Email = req.Email;
                    result.IsUsed = req.IsUsed;
                    result.UpdatedBy = req.CreatedBy;
                    result.UpdatedDate = DateTime.UtcNow;
                    result.PasswordHash = _encrypt.Encrypt(req.PasswordHash);
                    if (req.ImageFile != null && req.ImageFile is IFormFile imageFile)
                    {
                        var pathResult = await _upload.GetPathFile(2, imageFile, "employee", result.Image);
                        result.Image = pathResult.FilePath ?? "";
                    }
                    await _service.GetService<Employee>().UpdateAsync(result);
                    return new ResponseMessage(200, true, "updated successfully");
                }
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseMessage(500, false, $"DB Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }
    }
}
