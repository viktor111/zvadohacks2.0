using System.Security.Claims;

namespace ZvadoHacks.Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetUserId(this HttpContext httpContext)
        {
            return httpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;
        }
    }
}