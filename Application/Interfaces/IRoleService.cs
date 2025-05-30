using Application.Shared;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IRoleService
    {
        Task<PaginationResponse<RoleResponse>> GetAllRole(int pageSize, int currentPage);
        Task<ResponseData> GetRoleById(int id);
        Task<ResponseData> GetRoleDropdown();
        Task<ResponseMessage> SaveRole(RoleRequest req);
        Task<ResponseMessage> DeleteRole(int id);
    }
}
