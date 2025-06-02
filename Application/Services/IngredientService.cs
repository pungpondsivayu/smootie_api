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

namespace Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly UserCreator _Creator;

        public IngredientService(IServiceFactory service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _Creator = new UserCreator(service);
        }
        public async Task<ResponseMessage> DeleteIngredient(int id)
        {
            try
            {
                if (await _service.GetService<Ingredient>().GetByIdAsync(id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<Ingredient>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }
         
        public async Task<PaginationResponse<IngredientResponse>> GetAllIngredient(int pageSize, int currentPage)
        {
            var all = _mapper.Map<List<Ingredient> , List<IngredientResponse>>(_service.GetService<Ingredient>().GetAll().OrderByDescending(e => e.IngredientId).ToList());

            foreach (var item in all) item.CreatedBy = _Creator.getCreatorEmployee(int.Parse(item.CreatedBy));

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();


            return new PaginationResponse<IngredientResponse>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<ResponseData> GetIngredientDropdown()
        {
            var result =  _service.GetService<Ingredient>().GetAll().ToList().Select(e => new DropDownResponse
            {
                value = e.IngredientId,
                label = e.Name,
            });
            
            return new ResponseData(200, true, ""  , result);
        }

        public async Task<ResponseData> GetIngredientById(int id)
        {
            var result = _mapper.Map<Ingredient, IngredientResponse>(await _service.GetService<Ingredient>().GetByIdAsync(id));
            result.CreatedBy = _Creator.getCreatorEmployee(int.Parse(result.CreatedBy));
            if (result == null) return new ResponseData(404, false, "Data is NotFound");
            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseMessage> SaveIngredient(IngredientRequest req)
        {
            try
            {
                var result = _mapper.Map<IngredientRequest, Ingredient>(req);
                if (req.IngredientId == 0)
                {
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    await _service.GetService<Ingredient>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<Ingredient>().GetByIdAsync(req.IngredientId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    result.Name = req.Name;
                    result.Unit = req.Name;
                    result.IsUsed = req.IsUsed;
                    result.UpdatedBy = req.CreatedBy;
                    result.UpdatedDate = DateTime.UtcNow;
                    await _service.GetService<Ingredient>().UpdateAsync(result);
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
