namespace ZvadoHacks.Services
{
    public interface IPasswordManagerService
    {
        public string HashPassword(string password);

        public bool VerifyPassword(string password, string hash);
    }
}
