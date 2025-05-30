using System;
using System.Collections;
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
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly UploadFile _upload;
        private readonly UserCreator _Creator;

        public MenuService(IServiceFactory service, IMapper mapper, UploadFile uploadFile)
        {
            _service = service;
            _mapper = mapper;
            _upload = uploadFile;
            _Creator = new UserCreator(service);

        }

        public async Task<ResponseMessage> DeleteMenu(int id)
        {
            try
            {
                if (await _service.GetService<Menu>().GetByIdAsync(id) == null)
                {
                    return new ResponseMessage(404, false, "Data is Notfound");
                }

                await _service.GetService<Menu>().DeleteAsync(id);
                return new ResponseMessage(200, true, "Deleted successfully");
            }
            catch (Exception ex)
            {
                return new ResponseMessage(500, false, ex.Message);
            }
        }

        public async Task<PaginationResponse<MenuResponse>> GetAllMenu(string url, int pageSize, int currentPage, string name, int categoryId)
        {
            var all = (await _service.GetService<Menu>().GetAllWithIncludeAsync(m => m.Category))
            .Select(menu => new MenuResponse
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
                Price = menu.Price, 
                Image = menu.Image,
                CreatedBy = menu.CreatedBy,
                CreatedDate = menu.CreatedDate,
                CategoryId = menu.CategoryId,
                Category = new CategoryShortResponse
                {
                    CategoryId = menu.Category.CategoryId,
                    Name = menu.Category.Name
                },
                IsUsed = menu.IsUsed,
            }).OrderByDescending(e => e.MenuId).ToList();


            if (!string.IsNullOrEmpty(name))
            {
                all = all.Where(e => e.Name.Contains(name)).ToList();
            }

            if (categoryId > 0)
            {
                all = all.Where(e => e.CategoryId == categoryId).ToList();
            }

            foreach (var item in all)
            {
                item.CreatedBy = _Creator.getCreatorEmployee(int.Parse(item.CreatedBy));
                item.Image = _upload.CombineUrlPath(url, item.Image);
            };

            var totalCount = all.Count;
            var paged = all
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PaginationResponse<MenuResponse>(paged, totalCount, currentPage, pageSize);
        }

        public async Task<ResponseData> GetMenuById(string url, int id)
        {
            var result = _mapper.Map<Menu, MenuResponse>(await _service.GetService<Menu>().GetByIdAsync(id));
            result.CreatedBy = _Creator.getCreatorEmployee(int.Parse(result.CreatedBy));
            result.CreatedBy = _upload.CombineUrlPath(url, result.Image);
            if (result == null) return new ResponseData(404, false, "Data is NotFound");
            return new ResponseData(200, true, "", result);
        }

        public async Task<ResponseMessage> SaveMenu(MenuRequest req)
        {
            if (_service.GetService<MenuCategory>().GetAll().Where(e => e.CategoryId == req.CategoryId).FirstOrDefault() == null)
            {
                return new ResponseMessage(404, true, "Data is NotFound");
            }
            try
            {
                var result = _mapper.Map<MenuRequest, Menu>(req);
                if (req.MenuId == 0)
                {
                    result.CreatedBy = req.CreatedBy;
                    result.CreatedDate = DateTime.UtcNow;
                    if (req.ImageFile != null && req.ImageFile is IFormFile imageFile)
                    {
                        var pathResult = await _upload.GetPathFile(2, imageFile, "menu");
                        result.Image = pathResult.FilePath ?? "";
                    }
                    else
                    {
                        result.Image = "NoImage.png";
                    }
                    await _service.GetService<Menu>().AddAsync(result);
                    return new ResponseMessage(200, true, "Created successfully");
                }
                else
                {
                    result = await _service.GetService<Menu>().GetByIdAsync(req.MenuId);
                    if (result == null) return new ResponseMessage(404, false, "Data is NotFound");
                    result.Name = req.Name; 
                    result.Price = req.Price;
                    result.IsUsed = req.IsUsed;
                    result.UpdatedBy = req.CreatedBy;
                    result.CategoryId = req.CategoryId;
                    result.UpdatedDate = DateTime.UtcNow;
                    if (req.ImageFile != null && req.ImageFile is IFormFile imageFile)
                    {
                        var pathResult = await _upload.GetPathFile(2, imageFile, "menu", result.Image);
                        result.Image = pathResult.FilePath ?? "";
                    }
                    await _service.GetService<Menu>().UpdateAsync(result);
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
