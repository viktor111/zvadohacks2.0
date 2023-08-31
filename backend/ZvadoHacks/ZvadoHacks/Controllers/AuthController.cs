using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Infrastructure.Extensions;
using ZvadoHacks.Models.Auth;
using ZvadoHacks.Models.Shared;
using ZvadoHacks.Services;

namespace ZvadoHacks.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginRequest request)
        {
            var response = new BaseResponse<AuthLoginResponse>();

            try
            {
                var authReponse = await _authService.Login(request);

                if (authReponse != null)
                {
                    var claimsIdentity = authReponse.User.GenerateClaimsPrincipal();

                    await HttpContext.SignInAsync(claimsIdentity);

                    response.SetData(authReponse);

                    return Ok(response.Success());
                }

                return BadRequest(response.Error("Something went wrong with login"));
            }
            catch (Exception e)
            {
                return Ok(response.Error(e.Message));
            }
        }

        [HttpPost]
        [JwtAuthorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthRegisterRequest request)
        {
            var response = new BaseResponse<User>();

            try
            {
                var user = await _authService.Register(request);

                response.SetData(user);

                return Ok(response.Success());
            }
            catch (Exception e)
            {
                return Ok(response.Error(e.Message));
            }
        }
    }
}
