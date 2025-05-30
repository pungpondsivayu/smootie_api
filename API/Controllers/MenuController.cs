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
    public class MenuController : ControllerBase
    {

        private readonly ControllerHelper _helper;
        private readonly IMenuService _menuService;
        public MenuController(ControllerHelper helper, IMenuService menuService)
        {
            _helper = helper;
            _menuService = menuService;
        }

        [HttpGet("GetAllMenu")]
        public async Task<IActionResult> GetAllMenu([Required] int pageSize = 10, [Required] int currentPage = 1 , string name = "" , int categoryId = 0)
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return await _helper.HandleRequest(() => _menuService.GetAllMenu(url ,pageSize, currentPage , name , categoryId));
        }

        [HttpGet("GetMenu/{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return await _helper.HandleRequest(() => _menuService.GetMenuById(url, id));
        }

        [HttpPost("SaveMenu")]
        public async Task<IActionResult> SaveMenu([FromForm]MenuRequest req)
        {
            return await _helper.HandleRequest(() => _menuService.SaveMenu(req));
        }

        [HttpDelete("DeleteMenu")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            return await _helper.HandleRequest(() => _menuService.DeleteMenu(id));
        }

    }
}
