using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.Entities;
using Domain.Utility;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        public OrderService(IServiceFactory service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<ResponseData> CreateOrder(OrderRequest request)
        {
            try
            {
                var PickupTime = request.PickupTime;
                string DeliveryStatus = request.DeliveryStatus;
                if (request.Channel == "POS" && request.PickupMethod == "pickup")
                {
                    PickupTime = null;
                    DeliveryStatus = string.Empty;
                }
                else if (request.Channel == "Online" && request.PickupMethod == "pickup")
                {
                    DeliveryStatus = string.Empty;
                }
                else if (request.Channel == "Online" && request.PickupMethod == "pickup")
                {
                    PickupTime = null;
                }
                Order order = new()
                {
                    CustomerId = request.CustomerId == 0 ? null : request.CustomerId,
                    BranchId = request.BranchId,
                    OrderTime = DateTime.UtcNow,
                    Channel = request.Channel,
                    PaymentMethod = request.PaymentMethod,
                    PickupMethod = request.PickupMethod,
                    TotalAmount = request.TotalAmount,
                    DiscountAmount = request.DiscountAmount,
                    FinalAmount = request.FinalAmount,
                    CouponId = request.CouponId == 0 ? null : request.CouponId,
                    PointsEarned = request.PointsEarned,
                    PointsUsed = request.PointsUsed,
                    Status = request.Status,
                    PickupTime = PickupTime,
                    DeliveryStatus = DeliveryStatus
                };

                await _service.GetService<Order>().AddAsync(order);

                foreach (var item in request.OrderItems)
                {
                    OrderItem orderDetails = new()
                    {
                        OrderId = order.OrderId,
                        MenuId = item.MenuId,
                        Quantity = item.Quantity,
                        Price = item.Price
                    };
                    await _service.GetService<OrderItem>().AddAsync(orderDetails);
                }

                return new ResponseData(200, true, "Order created successfully", order);
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseData(500, false, $"DB Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseData(500, false, $"{ex.Message}");
            }
        }

    }
}
