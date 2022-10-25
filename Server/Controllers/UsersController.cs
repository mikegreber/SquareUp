using Microsoft.AspNetCore.Mvc;
using SquareUp.Server.Models;
using SquareUp.Server.Services.Users;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.ControllerEndpoints.Users;

namespace SquareUp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(GetAllUri)]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet(GetUserByIdUri)]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int id)
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }

        [HttpPost(PostRegisterUri)]
        public async Task<ActionResult<ServiceResponse<User>>> Register(RegisterRequest request)
        {
            var result = await _userService.Register(request);
            return Ok(result);
        }

        [HttpPost(PostLoginUri)]
        public async Task<ActionResult<ServiceResponse<UserBase>>> Login(LoginRequest request)
        {
            var result = await _userService.Login(request);
            return Ok(result);
        }
    }
}
