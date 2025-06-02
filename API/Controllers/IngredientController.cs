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
    public class IngredientController : ControllerBase
    {
        private readonly IGenericCrudService<Ingredient> _service;
        private readonly ControllerHelper _helper;
        private readonly IIngredientService _ingredientService;

        public IngredientController(IGenericCrudService<Ingredient> service ,ControllerHelper helper , IIngredientService ingredientService)
        {
            _service = service;
            _helper = helper;
            _ingredientService = ingredientService;
        }

        [HttpGet("GetAllIngredient")]
        public async Task<IActionResult> GetAllIngredien([Required] int pageSize = 10, [Required] int currentPage = 1)
        {
            return await _helper.HandleRequest(() => _ingredientService.GetAllIngredient(pageSize , currentPage));
        }
        
        [HttpGet("GetDropdown")]
        public async Task<IActionResult> GetIngredientDropdown()
        {
            return await _helper.HandleRequest(() => _ingredientService.GetIngredientDropdown());
        }

        [HttpGet("GetIngredient/{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            return await _helper.HandleRequest(() => _ingredientService.GetIngredientById(id));
        }

        [HttpPost("SaveIngredient")]
        public async Task<IActionResult> SaveIngredien(IngredientRequest req)
        {
            return await _helper.HandleRequest(() => _ingredientService.SaveIngredient(req));
        }

        [HttpDelete("DeleteIngredient")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            return await _helper.HandleRequest(() => _ingredientService.DeleteIngredient(id));
        }
    }
}
