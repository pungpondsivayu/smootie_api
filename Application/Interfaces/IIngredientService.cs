using Application.Shared;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IIngredientService
    {
        Task<PaginationResponse<IngredientResponse>> GetAllIngredient(int pageSize, int currentPage);
        Task<ResponseData> GetIngredientById(int id);
        Task<ResponseMessage> SaveIngredient(IngredientRequest req);
        Task<ResponseMessage> DeleteIngredient(int id);
    }
}
