using Application.Shared.Responses;
using Application.Shared;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IMenuRecipeService
    {
        Task<ResponseMessage> SaveMenuRecipe(MenuRecipeRequest req);
        Task<ResponseMessage> DeleteMenuRecipe(int id);
    }
}
