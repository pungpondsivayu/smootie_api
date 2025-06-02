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
    public class OrderController : ControllerBase
    {
        private readonly IGenericCrudService<MenuRecipe> _service;
        private readonly ControllerHelper _helper;
        private readonly IOrderService _orderService;

        public OrderController(IGenericCrudService<MenuRecipe> service, ControllerHelper helper, IOrderService orderService)
        {
            _helper = helper;
            _service = service;
            _orderService = orderService;
        }
        
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrder([Required] int pageSize = 10, [Required] int currentPage = 1, int BranchId = 0 , string StartDate = "", string EndDate = "")
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return await _helper.HandleRequest(() => _orderService.GetAllOrder(url ,pageSize , currentPage , BranchId , StartDate, EndDate));
        }

        
        [HttpPost("SaveOrder")]
        public async Task<IActionResult> SaveOrder(OrderRequest req)
        {
            return await _helper.HandleRequest(() => _orderService.CreateOrder(req));
        }



    }
}
