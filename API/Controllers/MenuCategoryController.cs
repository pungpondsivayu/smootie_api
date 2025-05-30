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
    public class MenuCategoryController : ControllerBase
    {
        private readonly IGenericCrudService<MenuCategory> _service;
        private readonly ControllerHelper _helper;
        private readonly IMenuCategoryService _menuCategoryService;

        public MenuCategoryController(IGenericCrudService<MenuCategory> service, ControllerHelper helper, IMenuCategoryService menuCategoryService)
        {
            _helper = helper;
            _service = service;
            _menuCategoryService = menuCategoryService;
        }

        [HttpGet("GetAllMenuCategory")]
        public async Task<IActionResult> GetAllMenuCategory([Required] int pageSize = 10, [Required] int currentPage = 1)
        {
            return await _helper.HandleRequest(() => _menuCategoryService.GetAllMenuCategory(pageSize, currentPage));
        }

        [HttpGet("GetMenuCategory/{id}")]
        public async Task<IActionResult> GetMenuCategory(int id)
        {
            return await _helper.HandleRequest(() => _menuCategoryService.GetMenuCategoryById(id));
        }

        [HttpGet("GetDropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            return await _helper.HandleRequest(() => _menuCategoryService.GetMenuCategoryDropdown());
        }

        [HttpPost("SaveMenuCategory")]
        public async Task<IActionResult> SaveMenuCategory(MenuCategoryRequest req)
        {
            return await _helper.HandleRequest(() => _menuCategoryService.SaveCategoryMenu(req));
        }

        [HttpDelete("DeleteMenuCategory")]
        public async Task<IActionResult> DeleteMenuCategory(int id)
        {
            return await _helper.HandleRequest(() => _menuCategoryService.DeleteCategoryMenu(id));
        }
    }
}
