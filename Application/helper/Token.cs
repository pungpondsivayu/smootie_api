using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.CustomResponse;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.helper
{
    public class Token
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceFactory _service;

        public Token(IConfiguration configuration , IServiceFactory service)
        {
            _configuration = configuration;
            _service = service; 
        }

        public string CreateToken(string Id, string role , string jwtTokenId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, Id),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, jwtTokenId),
                    new Claim(JwtRegisteredClaimNames.Sub, Id)
                }),
                Expires = DateTime.Now.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"] ?? "",
                Audience = _configuration["Jwt:Audience"] ?? ""
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<TokenResponse> RefreshAccessToken(TokenResponse token)
        {
            // Find an existing refresh token
            // ค้นหารีเฟรชโทเคนจาก database ว่ามีหรือไม่
            var existingRefreshToken = _service.GetService<Refreshtoken>().GetAll().FirstOrDefault(e => e.RefreshToken == token.RefreshToken);
            if (existingRefreshToken == null)
            {
                return new TokenResponse();
            }

            #region A ตรวจสอบความถูกต้องของโทเคนที่ส่งมาเข้า
            // Compare data from existing refresh and access token provided and if there is any missmatch then consider it as a fraud
            // ตรวจสอบว่า accesstoken ที่ส่งเข้ามาถูกต้องหรือไม่
            var accessTokenData = GetAccessTokenData(token.AccessToken);
            if (!accessTokenData.isSuccessful || accessTokenData.userId != existingRefreshToken.UserId.ToString()
                || accessTokenData.tokenId != existingRefreshToken.JwtTokenId)
            {
                existingRefreshToken.IsValid = false;
                await _service.GetService<Refreshtoken>().UpdateAsync(existingRefreshToken);
                return new TokenResponse();
            }
            // When someone tries to use not valid refresh token, fraud possible
            //เมื่อมีคนพยายามใช้โทเค็นรีเฟรชที่ไม่ถูกต้อง อาจเกิดการบุกรุกได้
            if (!existingRefreshToken.IsValid)
            {
                var chainRecords = _service.GetService<Refreshtoken>().GetAll().Where(u => u.UserId == existingRefreshToken.UserId && u.JwtTokenId == existingRefreshToken.JwtTokenId).ToList();

                foreach (var item in chainRecords)
                {
                    item.IsValid = false;
                }

                await _service.GetService<Refreshtoken>().UpdateRangeAsync(chainRecords);
                return new TokenResponse();
            }
            // If just expired then mark as invalid and return empty
            // ถ้าโทเคนหมดอายุ ให้ส่งค่าว่างกลับไป
            if (existingRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                existingRefreshToken.IsValid = false;
                await _service.GetService<Refreshtoken>().UpdateAsync(existingRefreshToken);
                return new TokenResponse();
            }
            #endregion A ตรวจสอบความถูกต้องของโทเคนที่ส่งมาเข้า
            #region B สร้าง accessToken refreshToken ให้ใหม่
            // replace old refresh with a new one with updated expire date
            // ให้อัพเดท refreshTokenใหม่ คือ ExpiresAt และ Refresh_Token
            var newRefreshToken = await CreateNewRefreshToken(existingRefreshToken.UserId, existingRefreshToken.JwtTokenId);

            // revoke existing refresh token
            existingRefreshToken.IsValid = false;
            await _service.GetService<Refreshtoken>().UpdateAsync(existingRefreshToken);

            // generate new access token
            // สร้าง accessToken ใหม่
            var user = _service.GetService<Employee>().GetAll().FirstOrDefault(u => u.EmployeeId == existingRefreshToken.UserId);
            if (user == null) return new TokenResponse();

            var newAccessToken = CreateToken(user.EmployeeId.ToString(), _service.GetService<Role>().GetByIdAsync(user.RoleId).Result.RoleName, existingRefreshToken.JwtTokenId);

            return new TokenResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
            #endregion B สร้าง accessToken refreshToken ให้ใหม่
        }

        public (bool isSuccessful, string userId, string tokenId) GetAccessTokenData(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwt = tokenHandler.ReadJwtToken(accessToken);
                var jwtTokenId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Jti).Value;
                var userId = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value;
                return (true, userId, jwtTokenId);

            }
            catch
            {
                return (false, null, null);
            }
        }

        public async Task<string> CreateNewRefreshToken(int userId, string tokenId)
        {
            Refreshtoken refreshToken = new()
            {
                IsValid = true,
                UserId = userId,
                JwtTokenId = tokenId,
                ExpiresAt = DateTime.UtcNow.AddDays(2),
                RefreshToken = Guid.NewGuid() + "-" + Guid.NewGuid(),
            };

            await _service.GetService<Refreshtoken>().AddAsync(refreshToken);
            return refreshToken.RefreshToken;
        }
    }
}
