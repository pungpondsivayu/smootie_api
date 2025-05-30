using System.Xml.Linq;
using Application.Interfaces;
using Application.Shared;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;

        public RoleService(IServiceFactory service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        public async Task<ResponseMessage> DeleteRole(int id)
        {
            try
            {
                if (_service.GetService<Role>().GetAll().FirstOrDefault(e => e.RoleId == id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<Role>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

        public async Task<PaginationResponse<RoleResponse>> GetAllRole(int pageSize, int currentPage)
        {
            var all = _mapper.Map<List<Role>, List<RoleResponse>>(_service.GetService<Role>().GetAll().ToList());

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            return new PaginationResponse<RoleResponse>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<ResponseData> GetRoleById(int id)
        {
            var result = _mapper.Map<Role, RoleResponse>(await _service.GetService<Role>().GetByIdAsync(id));
            if (result == null) return new ResponseData(404, false, "Data is NotFound");
            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseData> GetRoleDropdown()
        {
            var result = _service.GetService<Role>().GetAll().ToList().Select(e => new DropDownResponse
            {
                value = e.RoleId,
                label = e.RoleName,
            });
            
            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseMessage> SaveRole(RoleRequest req)
        {
            try
            {
                var result = _mapper.Map<RoleRequest, Role>(req);
                if (req.RoleId == 0)
                {
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    await _service.GetService<Role>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<Role>().GetByIdAsync(req.RoleId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    result.UpdatedBy = req.CreatedBy;
                    result.UpdatedDate = DateTime.UtcNow;
                    await _service.GetService<Role>().UpdateAsync(result);
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
