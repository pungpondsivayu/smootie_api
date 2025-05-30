using API.helper;
using Application.Interfaces;
using Domain.CustomRequest;
using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IGenericCrudService<Role> _service;
        private readonly ControllerHelper _helper;
        private readonly IRoleService _iroleService;

        public RoleController(IGenericCrudService<Role> service, ControllerHelper helper, IRoleService roleService)
        {
            _service = service;
            _helper = helper;
            _iroleService = roleService;
        }

        [HttpGet("GetAllRole")]
        public async Task<IActionResult> GetAllRole([Required] int pageSize = 10, [Required] int currentPage = 1)
        {
            return await _helper.HandleRequest(() => _iroleService.GetAllRole(pageSize, currentPage));
        }

        [HttpGet("GetRole/{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            return await _helper.HandleRequest(() => _iroleService.GetRoleById(id));
        }

        [HttpGet("GetDropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            return await _helper.HandleRequest(() => _iroleService.GetRoleDropdown());
        }

        [HttpPost("SaveRole")]
        public async Task<IActionResult> SaveRole(RoleRequest req)
        {
            return await _helper.HandleRequest(() => _iroleService.SaveRole(req));
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            return await _helper.HandleRequest(() => _iroleService.DeleteRole(id));
        }
    }
}
