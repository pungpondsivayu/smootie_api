using System.Xml.Linq;
using Application.helper;
using Application.Interfaces;
using Application.Shared;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly UserCreator _Creator;

        public BranchService(IServiceFactory service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _Creator = new UserCreator(service);
        }

        public async Task<ResponseMessage> DeleteBranch(int id)
        {
            try
            {
                if (await _service.GetService<Branch>().GetByIdAsync(id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<Branch>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

        public async Task<PaginationResponse<BranchResponse>> GetAllBranch(int pageSize, int currentPage, string province, string district, string subDistrict)
        {
            var all = _mapper.Map<List<Branch>, List<BranchResponse>>(_service.GetService<Branch>().GetAll().OrderByDescending(e => e.BranchId).ToList());

            if (!string.IsNullOrEmpty(province))
            {
                all = all.Where(e => e.Province.Contains(province)).ToList();
            }
            if (!string.IsNullOrEmpty(district))
            {
                all = all.Where(e => e.District.Contains(district)).ToList();
            }
            if (!string.IsNullOrEmpty(subDistrict))
            {
                all = all.Where(e => e.SubDistrict.Contains(subDistrict)).ToList();
            }

            foreach (var item in all) item.CreatedBy = _Creator.getCreatorEmployee(int.Parse(item.CreatedBy));

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<BranchResponse>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<ResponseData> GetBranchById(int id)
        {
            var result = _mapper.Map<Branch , BranchResponse>(await _service.GetService<Branch>().GetByIdAsync(id));
            result.CreatedBy = _Creator.getCreatorEmployee(int.Parse(result.CreatedBy));
            if (result == null) return new ResponseData(404, false, "Data is NotFound");
            return new ResponseData(200, true, "" , result);
        }

        public async Task<ResponseData> GetMenuBranchDropdown()
        {
            var result = _service.GetService<Branch>().GetAll().ToList().Select(e => new DropDownResponse
            {
                value = e.BranchId,
                label = e.BranchName,
            });

            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseMessage> SaveBranch(BranchRequest req)
        {
            try
            {
                var result = _mapper.Map<BranchRequest, Branch>(req);
                if (req.BranchId == 0)
                {
                    var Branchcount = _service.GetService<Branch>().GetAll().Where(e => e.Province.Contains(req.Province)).Count();
                    var name = $"{req.Province} สาขาที่ {Branchcount + 1}";
                    result.BranchName = name ;  
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    await _service.GetService<Branch>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<Branch>().GetByIdAsync(req.BranchId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    result.Province = req.Province;
                    result.District = req.District;
                    result.SubDistrict = req.SubDistrict;
                    result.PostalCode = req.PostalCode;
                    result.AddressDetail = req.AddressDetail;
                    result.UpdatedBy = req.CreatedBy;
                    result.UpdatedDate = DateTime.UtcNow;
                    result.IsUsed = req.IsUsed;
                    await _service.GetService<Branch>().UpdateAsync(result);
                    return new ResponseMessage(200, true, "updated successfully");
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

    }
}
