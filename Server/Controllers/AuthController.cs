using Microsoft.AspNetCore.Mvc;
using SquareUp.Server.Services.Auth;
using SquareUp.Server.Services.Users;

namespace SquareUp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        /*[HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<UserClient>>> Register(RegisterRequest request)
        {
            var response = await _authService.Register(
                new UserData { Name = request.Name, Email = request.Email },
                request.Password
            );

            if (!response.Success)
                return BadRequest(new ServiceResponse<UserClient>(message: response.Message));

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<UserClient>>> Login(LoginRequest request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }*/
    }
}
