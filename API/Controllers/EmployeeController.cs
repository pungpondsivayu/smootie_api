using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
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
    public class EmployeeController : ControllerBase
    {
        private readonly IGenericCrudService<Employee> _service;
        private readonly ControllerHelper _helper;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IGenericCrudService<Employee> service, ControllerHelper helper, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _service = service;
            _helper = helper;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee([Required] int pageSize = 10, [Required] int currentPage = 1, int BranchId = 0, int RoleId = 0, string search = "", string status = "")
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return await _helper.HandleRequest(() => _employeeService.GetAllEmployee(url, pageSize, currentPage, BranchId, RoleId, search , status));
        }

        [HttpGet("GetEmployee/{id}")]
        public async Task<IActionResult> GetAllBranch(int id)
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return await _helper.HandleRequest(() => _employeeService.GetEmployeeById(url , id));
        }

        [HttpPost("SaveEmployee")]
        public async Task<IActionResult> SaveEmployee([FromForm]EmployeeRequest req)
        {
            return await _helper.HandleRequest(() => _employeeService.SaveEmployee(req));
        }

        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            return await _helper.HandleRequest(() => _employeeService.DeleteEmployee(id));
        }

    }
}
