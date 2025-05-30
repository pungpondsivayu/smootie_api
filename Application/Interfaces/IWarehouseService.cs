using Application.Shared;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IWarehouseService
    {
        Task<PaginationResponse<StockResponse>> GetAllWarehouse(int pageSize, int currentPage, int BranchId);
        Task<PaginationResponse<StockRequestResponse>> GetAllTransection(int pageSize, int currentPage, int BranchId, string StartDate , string EndDate , string Status , string RequestType);
        Task<ResponseMessage> CreateTransection(TransectionRequest req);
        Task<ResponseMessage> ChangeStatus(int userId , int StockRequestId , string status);
        Task<ResponseMessage> Transection(int StockRequestId);

    }
}
