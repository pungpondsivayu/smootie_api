using Application.Shared.Responses;
using Domain.CustomRequest;
using Domain.CustomResponse;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseData> Login(string url , LoginReq req);
        Task<ResponseData> RefreshAccessToken(TokenResponse tokenReq);
    }
}
