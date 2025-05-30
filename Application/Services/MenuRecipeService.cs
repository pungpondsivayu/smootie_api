using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Shared;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;

namespace Application.Services
{
    public class MenuRecipeService : IMenuRecipeService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;

        public MenuRecipeService(IServiceFactory service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<ResponseMessage> DeleteMenuRecipe(int id)
        {
            try
            {
                if (_service.GetService<MenuRecipe>().GetByIdAsync(id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<MenuRecipe>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

        public async Task<ResponseMessage> SaveMenuRecipe(MenuRecipeRequest req)
        {
            if (_service.GetService<Menu>().GetAll().Where(e => e.MenuId == req.MenuId).FirstOrDefault() == null ||
                _service.GetService<Ingredient>().GetAll().FirstOrDefault(e => e.IngredientId == req.IngredientId) == null)
            {
                return new ResponseMessage(404, true, "Data is NotFound");
            }
            try
            {
                var result = _mapper.Map<MenuRecipeRequest, MenuRecipe>(req);
                if (req.RecipeId == 0)
                {
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    await _service.GetService<MenuRecipe>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<MenuRecipe>().GetByIdAsync(req.RecipeId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    result.UpdatedBy = req.CreatedBy;
                    result.UpdatedDate = DateTime.UtcNow;
                    await _service.GetService<MenuRecipe>().UpdateAsync(result);
                    return new ResponseMessage(200, true, "updated successfully");
                }
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }
    }
}
