using System.Security.Claims;
using ZvadoHacks.Data.Entities;

namespace ZvadoHacks.Infrastructure.Extensions
{
    public static class UserExtensions
    {
        private static List<Claim> GenerateUserClaims(this User user)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Upn, user.Id),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("CreatedAt", user.CreatedAt.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };
        }

        public static ClaimsIdentity GenerateClaimsIdentity(this User user)
        {
            var claims = user.GenerateUserClaims();
            return new ClaimsIdentity(claims, "Token");
        }

        public static ClaimsPrincipal GenerateClaimsPrincipal(this User user)
        {
            var claimsIdentity = user.GenerateClaimsIdentity();
            return new ClaimsPrincipal(claimsIdentity);
        }
    }
}
