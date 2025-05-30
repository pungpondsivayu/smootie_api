using Application.Shared;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IMenuService
    {
        Task<PaginationResponse<MenuResponse>> GetAllMenu(string url, int pageSize, int currentPage  , string name , int categoryId);
        Task<ResponseData> GetMenuById(string url, int id);
        Task<ResponseMessage> SaveMenu(MenuRequest req);
        Task<ResponseMessage> DeleteMenu(int id);
    }
}
