using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IMenuCategoryService
    {
        Task<PaginationResponse<MenuCategoryResponse>> GetAllMenuCategory(int pageSize, int currentPage);
        Task<ResponseData> GetMenuCategoryById(int id);
        Task<ResponseData> GetMenuCategoryDropdown();
        Task<ResponseMessage> SaveCategoryMenu(MenuCategoryRequest req);
        Task<ResponseMessage> DeleteCategoryMenu(int id);
    }
}
