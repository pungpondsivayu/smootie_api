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
    public class MenuRecipeController : ControllerBase
    {
        private readonly IGenericCrudService<MenuRecipe> _service;
        private readonly ControllerHelper _helper;
        private readonly IMenuRecipeService _menuRecipeService;

        public MenuRecipeController(IGenericCrudService<MenuRecipe> service, ControllerHelper helper, IMenuRecipeService menuRecipeService)
        {
            _helper = helper;
            _service = service;
            _menuRecipeService = menuRecipeService;
        }

        [HttpPost("SaveMenuRecipe")]
        public async Task<IActionResult> SaveMenuRecipe([FromForm] MenuRecipeRequest req)
        {
            return await _helper.HandleRequest(() => _menuRecipeService.SaveMenuRecipe(req));
        }

        [HttpDelete("DeleteMenuRecipe")]
        public async Task<IActionResult> DeleteMenuRecipe(int id)
        {
            return await _helper.HandleRequest(() => _menuRecipeService.DeleteMenuRecipe(id));
        }

    }
}
