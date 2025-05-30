using System.ComponentModel.DataAnnotations;
using API.helper;
using Application.Interfaces;
using Application.Services;
using Domain.CustomRequest;
using Domain.Entities;
using Domain.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IGenericCrudService<Branch> _service;
        private readonly ControllerHelper _helper;
        private readonly IBranchService _branchService;

        public BranchController(IGenericCrudService<Branch> service, ControllerHelper helper, IBranchService branchService)
        {
            _branchService = branchService;
            _service = service;
            _helper = helper;
        }

        [HttpGet("GetAllBranch")]
        public async Task<IActionResult> GetAllBranch([Required] int pageSize = 10, [Required] int currentPage = 1, string province = "", string district = "", string subDistrict = "")
        {
            return await _helper.HandleRequest(() => _branchService.GetAllBranch(pageSize, currentPage, province, district, subDistrict));
        }

        [HttpGet("GetBranch/{id}")]
        public async Task<IActionResult> GetAllBranch(int id)
        {
            return await _helper.HandleRequest(() => _branchService.GetBranchById(id));
        }

        [HttpGet("GetDropdown")]
        public async Task<IActionResult> GetDropdown()
        {
            return await _helper.HandleRequest(() => _branchService.GetMenuBranchDropdown());
        }

        [HttpPost("SaveBranch")]
        public async Task<IActionResult> SaveBranch(BranchRequest req)
        {
            return await _helper.HandleRequest(() => _branchService.SaveBranch(req));
        }

        [HttpDelete("DeleteBranch")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            return await _helper.HandleRequest(() => _branchService.DeleteBranch(id));
        }
    }
}
