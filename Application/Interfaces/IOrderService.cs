using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseData> CreateOrder(OrderRequest request);
        Task<PaginationResponse<OrderResponse>> GetAllOrder(string url ,int pageSize, int currentPage , int BranchId , string StartDate, string EndDate);
    }
}
