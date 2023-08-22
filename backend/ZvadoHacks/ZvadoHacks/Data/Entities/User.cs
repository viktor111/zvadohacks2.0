using ZvadoHacks.Models.Types;

namespace ZvadoHacks.Data.Entities
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Username { get; set; }

        public RoleType Role { get; set; } = RoleType.User;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
