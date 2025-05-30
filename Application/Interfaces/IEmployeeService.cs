using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Shared.Responses;
using Application.Shared;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<PaginationResponse<EmployeeResponse>> GetAllEmployee(string url ,int pageSize, int currentPage, int BranchId , int RoleId , string search , string status);
        Task<ResponseData> GetEmployeeById(string url, int id);
        Task<ResponseMessage> SaveEmployee(EmployeeRequest req);
        Task<ResponseMessage> DeleteEmployee(int id);
    }
}
