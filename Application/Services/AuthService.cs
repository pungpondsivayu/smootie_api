using Application.helper;
using Application.Interfaces;
using Application.Shared.Responses;
using AutoMapper;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IServiceFactory _service;
        private readonly IMapper _mapper;
        private readonly Cryptographic _encrypt;
        private readonly Token _token;
        private readonly UploadFile _upload;

        public AuthService(IServiceFactory service, IMapper mapper, IConfiguration configuration, UploadFile uploadFile)
        {
            _service = service;
            _encrypt = new Cryptographic();
            _mapper = mapper;
            _token = new Token(configuration , service);
            _upload = uploadFile;
        }

        public async Task<ResponseData> Login(string url, LoginReq req)
        {
            try
            {
                var password = _encrypt.Encrypt(req.Password);
                Employee? employee = _service.GetService<Employee>().GetAll().Where(a => a.Email == req.Email)
                                                               .Where(a => a.PasswordHash == password).Where(e => e.Status == "Active").FirstOrDefault(e => e.IsUsed == true);

                if (employee == null)
                {
                    throw new Exception("Please check your username and password again.");
                }

                var jwtTokenId = $"JTI{Guid.NewGuid()}";
                var refreshToken = await _token.CreateNewRefreshToken(employee.EmployeeId, jwtTokenId);
                var role = await _service.GetService<Role>().GetByIdAsync(employee.RoleId);
                UserResponse data = new UserResponse()
                {
                    Id = employee.EmployeeId,
                    FullName = employee.Fullname,
                    Email = employee.Email,
                    Profile = _upload.CombineUrlPath(url, employee.Image),
                    Role = role.RoleName,
                    BranchId = employee.BranchId ?? 0,
                    Token = new TokenResponse()
                    {
                        AccessToken = _token.CreateToken(employee.EmployeeId.ToString(), role.RoleName , jwtTokenId),
                        RefreshToken = refreshToken
                    }
                };
                return new ResponseData(200, true, "Signin successfully", data);
            }
            catch (Exception ex)
            {
                return new ResponseData(500, false, ex.Message);
            }
        }

        public async Task<ResponseData> RefreshAccessToken(TokenResponse tokenReq)
        {
            try
            {
                var token = await _token.RefreshAccessToken(tokenReq);
                if (token == null || string.IsNullOrEmpty(token.AccessToken)) { 
                    return new ResponseData(400, false, "Token Invalid");
                }
                return new ResponseData(200, true, "" , token);
            }
            catch (Exception ex)
            {

                return new ResponseData(500 , false, ex.Message);
            }
        }
    }
}
