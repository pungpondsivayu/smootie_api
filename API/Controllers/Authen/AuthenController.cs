using System.Net;
using API.helper;
using Application.Interfaces;
using Azure;
using Domain.CustomRequest;
using Domain.CustomResponse;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Authen
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly ControllerHelper _helper;
        private readonly IAuthService _authService;

        public AuthenController(ControllerHelper helper, IAuthService authService)
        {
            _authService = authService; 
            _helper = helper;   
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReq req)
        {
            var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
            return await _helper.HandleRequest(() => _authService.Login(url, req));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> GetNewTokenFromRefreshToken([FromBody] TokenResponse token)
        {
            return await _helper.HandleRequest(() => _authService.RefreshAccessToken(token));
        }
    }
}
