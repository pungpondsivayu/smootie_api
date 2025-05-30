using Application.Shared.Responses;
using Application.Shared;
using Domain.CustomRequest;
using Domain.CustomResponse;
using System.ComponentModel.DataAnnotations;

namespace Application.Interfaces
{
    public interface IBranchService
    {
        Task<PaginationResponse<BranchResponse>> GetAllBranch(int pageSize, int currentPage , string province, string district , string subDistrict);
        Task<ResponseData> GetBranchById(int id);
        Task<ResponseData> GetMenuBranchDropdown();
        Task<ResponseMessage> SaveBranch(BranchRequest req);
        Task<ResponseMessage> DeleteBranch(int id);
    }
}
