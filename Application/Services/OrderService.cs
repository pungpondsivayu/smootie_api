using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.helper;
using Application.Interfaces;
using Application.Shared;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;
using Domain.Utility;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly UploadFile _upload;
        public OrderService(IServiceFactory service, IMapper mapper , UploadFile upload)
        {
            _service = service;
            _mapper = mapper;
            _upload = upload;
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
                else if (request.Channel == "Online" && request.PickupMethod == "delivery")
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
                    var menuRecipes = _service.GetService<MenuRecipe>().GetAll().Where(e => e.MenuId == item.MenuId).ToList();
                    List<CounterStock> updateCounter = new List<CounterStock>();
                    foreach (var menuRecipe in menuRecipes)
                    {
                        var counterStock = _service.GetService<CounterStock>().GetAll()
                            .FirstOrDefault(e => e.IngredientId == menuRecipe.IngredientId);
                        var toal = counterStock.Quantity -= item.Quantity;
                        if (toal <= 0)
                        {
                            return new ResponseData(401, false, "Something went wrong");
                        }

                        counterStock.Quantity -= item.Quantity;
                        updateCounter.Add(counterStock);
                    }

                    if (updateCounter.Count > 0)
                    {
                        await _service.GetService<CounterStock>().UpdateRangeAsync(updateCounter);
                    }
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

        public async Task<PaginationResponse<OrderResponse>> GetAllOrder(string url , int pageSize, int currentPage , int BranchId , string StartDate, string EndDate)
        {
            var all = (await _service.GetService<Order>().GetAllWithIncludeAsync(e => e.OrderItems)).Select(e => new OrderResponse()
            {
                OrderId  = e.OrderId,
                CustomerId = e.CustomerId,
                BranchId = e.BranchId ?? 0,
                OrderTime = e.OrderTime ?? DateTime.UtcNow,
                Channel = e.Channel,
                PaymentMethod = e.PaymentMethod,
                PickupMethod = e.PickupMethod,
                TotalAmount = e.TotalAmount ?? 0,
                DiscountAmount = e.DiscountAmount ?? 0,
                FinalAmount = e.FinalAmount ?? 0,
                CouponId = e.CouponId ?? 0,
                PointsEarned = e.PointsEarned ?? 0,
                PointsUsed = e.PointsUsed ?? 0,
                Status = e.Status,
                PickupTime = e.PickupTime,
                DeliveryStatus = e.DeliveryStatus,
            }).ToList();
            
            foreach (var order in all)
            {
               var item = (await _service.GetService<OrderItem>().GetAllWithIncludeAsync((e => e.Menu))).Where(e => e.OrderId == order.OrderId).Select(e => new OrderItemResponse
               {
                   OrderId = order.OrderId, 
                   Name = e.Menu.Name,
                   Image = _upload.CombineUrlPath(url ,e.Menu.Image),
                   Price = e.Menu.Price ?? 0,
                   Quantity = e.Quantity ?? 0
               }).ToList();
               order.OrderItems = item;
            }
            
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (!string.IsNullOrWhiteSpace(StartDate))
            {
                if (DateTime.TryParse(StartDate, out var parsedStart))
                    startDate = parsedStart.Date;
            }

            if (!string.IsNullOrWhiteSpace(EndDate))
            {
                if (DateTime.TryParse(EndDate, out var parsedEnd))
                    endDate = parsedEnd.Date.AddDays(1).AddTicks(-1); // รวมทั้งวันของ EndDate
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                all = all.Where(e => e.OrderTime >= startDate.Value && e.OrderTime <= endDate.Value).ToList();
            }
            else if (startDate.HasValue)
            {
                all = all.Where(e => e.OrderTime >= startDate.Value).ToList();
            }
            else if (endDate.HasValue)
            {
                all = all.Where(e => e.OrderTime <= endDate.Value).ToList();
            }

            if (BranchId != 0)
            {
                all = all.Where(e => e.BranchId == BranchId).ToList();
            }
            
            
            
            

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            
            return new PaginationResponse<OrderResponse>(paged, totalCount, currentPage, pageSize);
        }
    }
}
