using Euroins.Payment.Data.Repositories;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Models.Auth;

namespace ZvadoHacks.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordManagerService _passwordManagerService;
        private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
        private readonly IRepository<User> _userRepository;

        public AuthService(
            IPasswordManagerService passwordManagerService,
            IJwtTokenGeneratorService jwtTokenGeneratorService, 
            IRepository<User> userRepository)
        {
            _passwordManagerService = passwordManagerService;
            _jwtTokenGeneratorService = jwtTokenGeneratorService;
            _userRepository = userRepository;
        }

        public async Task<AuthLoginResponse> Login(AuthLoginRequest request)
        {
            var user = await _userRepository.FindOne(x => x.Username == request.Login || x.Email == request.Login);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var isPasswordValid = _passwordManagerService.VerifyPassword(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Password not mathc");
            }

            var token = _jwtTokenGeneratorService.GenerateToken(user);

            return new AuthLoginResponse { Token = token };
        }

        public async Task<User> Register(AuthRegisterRequest request)
        {
            var existingUserEmail = await _userRepository.FindOne(x => x.Email == request.Email);

            if(existingUserEmail is not null)
            {
                throw new Exception("Email taken");
            }

            var existingUserUserame = await _userRepository.FindOne(x => x.Username == request.Username);

            if(existingUserUserame is not null)
            {
                throw new Exception("Username taken");
            }

            var passwordHash = _passwordManagerService.HashPassword(request.Password);

            var newUser = new User() 
            {
                Email = request.Email,
                Username = request.Username,
                PasswordHash = passwordHash,
            };

            var addedUser = await _userRepository.Add(newUser);

            if(addedUser is null)
            {
                throw new Exception("User not registerd");
            }

            return addedUser;
        }
    }
}
