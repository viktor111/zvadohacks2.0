using ZvadoHacks.Data.Entities;
using ZvadoHacks.Models.Auth;

namespace ZvadoHacks.Services
{
    public interface IAuthService
    {
        public Task<User> Register(AuthRegisterRequest request);
        public Task<AuthLoginResponse> Login(AuthLoginRequest request);
    }
}
