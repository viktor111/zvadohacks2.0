using ZvadoHacks.Data.Entities;

namespace ZvadoHacks.Services
{
    public interface IJwtTokenGeneratorService
    {
        public string GenerateToken(User user);
    }
}
