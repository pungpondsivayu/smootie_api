using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly UserCreator _Creator;

        public MenuCategoryService(IServiceFactory service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
            _Creator = new UserCreator(service);
        }
        public async Task<ResponseMessage> DeleteCategoryMenu(int id)
        {
            try
            {
                if (await _service.GetService<MenuCategory>().GetByIdAsync(id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<MenuCategory>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

        public async Task<PaginationResponse<MenuCategoryResponse>> GetAllMenuCategory(int pageSize, int currentPage)
        {
            var all = _mapper.Map<List<MenuCategory>, List<MenuCategoryResponse>>(_service.GetService<MenuCategory>().GetAll().OrderByDescending(e => e.CategoryId).ToList());

            var totalCount = all.Count;

            foreach (var item in all) item.CreatedBy = _Creator.getCreatorEmployee(int.Parse(item.CreatedBy));
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<MenuCategoryResponse>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<ResponseData> GetMenuCategoryById(int id)
        {
            var result = _mapper.Map<MenuCategory, MenuCategoryRequest>(await _service.GetService<MenuCategory>().GetByIdAsync(id));
            if (result == null) return new ResponseData(404, false, "Data is NotFound");
            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseData> GetMenuCategoryDropdown()
        {
            var result = _service.GetService<MenuCategory>().GetAll().ToList().Select(e => new DropDownResponse
            {
                value = e.CategoryId,
                label = e.Name,
            });

            return new ResponseData(200, true, ""  , result);
        }


        public async Task<ResponseMessage> SaveCategoryMenu(MenuCategoryRequest req)
        {
            try
            {
                var result = _mapper.Map<MenuCategoryRequest, MenuCategory>(req);
                if (req.CategoryId == 0)
                {
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    await _service.GetService<MenuCategory>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<MenuCategory>().GetByIdAsync(req.CategoryId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    result.Name = req.Name;
                    result.Description = req.Description;
                    result.IsUsed = req.IsUsed;
                    result.UpdatedBy = req.CreatedBy;
                    result.UpdatedDate = DateTime.UtcNow;
                    await _service.GetService<MenuCategory>().UpdateAsync(result);
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
