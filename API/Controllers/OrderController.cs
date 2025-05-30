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

        [HttpPost("SaveOrder")]
        public async Task<IActionResult> SaveOrder(OrderRequest req)
        {
            return await _helper.HandleRequest(() => _orderService.CreateOrder(req));
        }



    }
}
