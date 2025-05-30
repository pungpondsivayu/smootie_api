using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;

namespace Application.helper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Menu
            CreateMap<Menu, MenuResponse>();
            CreateMap<Menu, MenuRequest>().ReverseMap();

            // MenuCategory
            CreateMap<MenuCategory, MenuCategoryResponse>();
            CreateMap<MenuCategory, MenuCategoryRequest>().ReverseMap();

            // Ingredient
            CreateMap<Ingredient, IngredientResponse>();
            CreateMap<Ingredient, IngredientRequest>().ReverseMap();

            // MenuRecipe
            CreateMap<MenuRecipe, MenuRecipeRequest>().ReverseMap();

            // role
            CreateMap<Role, RoleResponse>();
            CreateMap<Role, RoleRequest>().ReverseMap();

            // Branch
            CreateMap<Branch, BranchResponse>();
            CreateMap<Branch, BranchRequest>().ReverseMap();

            // Employee
            CreateMap<Employee, EmployeeResponse>();
            CreateMap<Employee, EmployeeRequest>().ReverseMap();

            // Employee
            //CreateMap<Employee, EmployeeResponse>();
            CreateMap<OrderRequest, Order>()
                .ForMember(dest => dest.OrderId, opt => opt.Ignore()) // ปล่อยให้ DB สร้างเอง
                .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems));

            CreateMap<OrderItemRequest, OrderItem>()
                .ForMember(dest => dest.OrderItemId, opt => opt.Ignore()) // ปล่อยให้ DB สร้างเอง
                .ForMember(dest => dest.Order, opt => opt.Ignore()); // เพราะเราจะ set เองใน service

        }
    }
}
