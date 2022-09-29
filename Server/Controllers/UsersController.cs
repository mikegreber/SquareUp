using Microsoft.AspNetCore.Mvc;
using SquareUp.Server.Models;
using SquareUp.Server.Services.Users;
using SquareUp.Shared;
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

        [HttpGet(GetAll)]
        public async Task<ActionResult<ServiceResponse<List<UserClient>>>> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet(GetUserById)]
        public async Task<ActionResult<ServiceResponse<UserClient>>> GetUser(int id)
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }

        [HttpPost(PostRegister)]
        public async Task<ActionResult<ServiceResponse<UserClient>>> Register(RegisterRequest request)
        {
            var result = await _userService.Register(request);
            return Ok(result);
        }

        [HttpPost(PostLogin)]
        public async Task<ActionResult<ServiceResponse<UserClient>>> Login(LoginRequest request)
        {
            var result = await _userService.Login(request);
            return Ok(result);
        }
    }
}
