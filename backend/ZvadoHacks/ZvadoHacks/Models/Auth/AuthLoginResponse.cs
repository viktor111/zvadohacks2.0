using System.Security.Claims;
using ZvadoHacks.Data.Entities;

namespace ZvadoHacks.Models.Auth
{
    public class AuthLoginResponse
    {
        public string Token { get; set; }

        public User User { get; set; }

        public List<ClaimsIdentity> UserClaims { get; set; }
    }
}
