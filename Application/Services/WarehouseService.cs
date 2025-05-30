
using Application.Interfaces;
using Application.Shared;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        public WarehouseService(IServiceFactory service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        public async Task<PaginationResponse<StockResponse>> GetAllWarehouse(int pageSize, int currentPage, int BranchId,  string name)
        {
            var all = (await _service.GetService<StockItem>()
                        .GetAllWithIncludeAsync(e => e.Ingredient)).Select(e => new StockResponse
                        {
                            StockId = e.StockId,
                            BranchId = e.BranchId,
                            IngredientId = e.IngredientId,
                            Quantity = e.Quantity,
                            Ingredient = new ShotIngredientResponse
                            {
                                Name = e.Ingredient.Name,
                                Unit = e.Ingredient.Unit,
                            }
                        }).ToList();

            if (BranchId != 0)
            {
                all = all.Where(e => e.BranchId == BranchId).ToList();
            }

            if (!string.IsNullOrEmpty(name))
            {
                all = all.Where(e => e.Ingredient.Name == name).ToList();

            }

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<StockResponse>(paged, totalCount, currentPage, pageSize);
        }


        public async Task<ResponseMessage> CreateTransection(TransectionRequest req)
        {
            if (await _service.GetService<Employee>().GetByIdAsync(req.EmployeeId) == null ||
                await _service.GetService<Branch>().GetByIdAsync(req.BranchId) == null)
            {
                return new ResponseMessage(404, true, "Data is NotFound");
            }
            try
            {
                StockRequest Stock = new()
                {
                    EmployeeId = req.EmployeeId,
                    BranchId = req.BranchId,
                    RequestDate = DateTime.UtcNow,
                    Status = req.Status == "request" ? string.Empty : "approved",
                    ApprovedBy = "",
                    ApprovedDate = null,
                    RequestType = req.RequestType,
                };

                await _service.GetService<StockRequest>().AddAsync(Stock);

                foreach (var item in req.stockItem)
                {
                    StockRequestItem stockRequestItem = new()
                    {
                        RequestId = Stock.RequestId,
                        IngredientId = item.IngredientId,
                        Quantity = item.Quantity,
                    };
                    await _service.GetService<StockRequestItem>().AddAsync(stockRequestItem);
                }

                return new ResponseMessage(200, true, "Stock created successfully");
            }
            catch (DbUpdateException dbEx)
            {
                return new ResponseMessage(500, false, $"DB Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, $"{ex.Message}");
            }
        }

        public async Task<PaginationResponse<StockRequestResponse>> GetAllTransection(int pageSize, int currentPage, int BranchId, string StartDate, string EndDate, string Status, string RequestType)
        {
            var all = _service.GetService<StockRequest>().GetAll().Select(e => new StockRequestResponse
            {
                RequestId = e.RequestId,
                EmployeeId = e.EmployeeId,
                BranchId = e.BranchId,
                RequestDate = e.RequestDate,
                Status = e.Status,
                ApprovedBy = e.Status,
                ApprovedDate = e.ApprovedDate,
                RequestType = e.RequestType,
                StockRequestItem = e.StockRequestItems.Where(a => a.RequestId == e.RequestId).Select(s => new StockRequestItemResponse
                {
                    ItemId = s.ItemId,
                    Name = s.Ingredient.Name,
                    Quantity = s.Quantity,
                    Unit = s.Ingredient.Unit,
                }).ToList()
            }).ToList();

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
                all = all.Where(e => e.RequestDate >= startDate.Value && e.RequestDate <= endDate.Value).ToList();
            }
            else if (startDate.HasValue)
            {
                all = all.Where(e => e.RequestDate >= startDate.Value).ToList();
            }
            else if (endDate.HasValue)
            {
                all = all.Where(e => e.RequestDate <= endDate.Value);
            }

            if (BranchId != 0)
            {
                all = all.Where(e => e.BranchId == BranchId).ToList();
            }

            if (!string.IsNullOrWhiteSpace(Status))
            {
                all = all.Where(e => e.Status == Status).ToList();
            }

            if (!string.IsNullOrWhiteSpace(RequestType))
            {
                all = all.Where(e => e.RequestType == RequestType).ToList();
            }

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<StockRequestResponse>(paged, totalCount, currentPage, pageSize);
        }
    }
}
