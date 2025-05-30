using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseData> CreateOrder(OrderRequest request);
    }
}
