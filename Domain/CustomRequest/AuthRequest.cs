using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Domain.CustomRequest
{
    public class LoginReq
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
