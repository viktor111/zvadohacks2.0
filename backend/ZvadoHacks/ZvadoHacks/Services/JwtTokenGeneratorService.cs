using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Models.Types;

namespace ZvadoHacks.Services
{
    public class JwtTokenGeneratorService : IJwtTokenGeneratorService
    {
        private readonly IConfiguration configuration;


        public JwtTokenGeneratorService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddDays(1),
                IssuedAt = DateTime.Now,
                SigningCredentials = GetSigningCredentials(),
                Subject = new ClaimsIdentity(GetClaims(user))
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var serializedToken = tokenHandler.WriteToken(token);

            return serializedToken;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtKey = configuration.GetValue<string>("jwtKey");
            if (string.IsNullOrEmpty(jwtKey))
            {
                throw new Exception("jwtKey cannot be null while generating the token");
            }
            var key = Encoding.ASCII.GetBytes(jwtKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            return credentials;
        }

        private Claim[] GetClaims(User user)
        {
            var claims = new[] {

            new Claim(ClaimTypes.Upn, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, GetUserRole(user.Role)),
        };

            return claims;
        }

        private string GetUserRole(RoleType roleType)
        {
            return roleType.ToString();
        }
    }
}
