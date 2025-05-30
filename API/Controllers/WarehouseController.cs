using System.ComponentModel.DataAnnotations;
using API.helper;
using Application.Interfaces;
using Application.Services;
using Domain.CustomRequest;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IGenericCrudService<StockItem> _service;
        private readonly ControllerHelper _helper;
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IGenericCrudService<StockItem> service, ControllerHelper helper, IWarehouseService warehouseService)
        {
            _helper = helper;
            _service = service;
            _warehouseService = warehouseService;
        }

        [HttpGet("GetAllWarehouse")]
        public async Task<IActionResult> GetAllWarehouse([Required] int pageSize = 10, [Required] int currentPage = 1 , int BranchId = 0)
        {
            return await _helper.HandleRequest(() => _warehouseService.GetAllWarehouse(pageSize, currentPage , BranchId));
        }

        [HttpGet("GetAllTransection")]
        public async Task<IActionResult> GetAllRole([Required] int pageSize = 10, [Required] int currentPage = 1, int BranchId = 0, string StartDate = "", string EndDate = "", string Status = "", string RequestType = "")
        {
            return await _helper.HandleRequest(() => _warehouseService.GetAllTransection(pageSize, currentPage, BranchId, StartDate , EndDate , Status , RequestType));
        }

        [HttpPost("CreateTransection")]
        public async Task<IActionResult> CreateTransection(TransectionRequest req)
        {
            return await _helper.HandleRequest(() => _warehouseService.CreateTransection(req));
        }
        
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int userId , int stockRequestId , string status)
        {
            return await _helper.HandleRequest(() => _warehouseService.ChangeStatus(userId , stockRequestId , status));
        }
    }
}
