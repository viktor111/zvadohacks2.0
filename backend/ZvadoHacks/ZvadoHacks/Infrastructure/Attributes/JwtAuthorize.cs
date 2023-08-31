using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class JwtAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    public JwtAuthorizeAttribute(string role)
    {
        _role = role;

    }

    public JwtAuthorizeAttribute()
    {
        _role = "User";
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        var jwt = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrWhiteSpace(jwt))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!IsValidToken(jwt, configuration))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if(context.HttpContext.User.Claims.Any())
        {
            var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if(roleClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_role != "Admin" && roleClaim.Value != _role)
            {
                context.Result = new StatusCodeResult(403);
                return;
            }
        }
    }

    private bool IsValidToken(string token, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtKey = configuration.GetValue<string>("jwtKey");
        var key = Encoding.ASCII.GetBytes(jwtKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out _);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
