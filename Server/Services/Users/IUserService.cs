using SquareUp.Server.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Users;

public interface IUserService
{
    Task<ServiceResponse<User>> GetCurrentUser();
    Task<ServiceResponse<List<UserClient>>> GetUsers();
    Task<ServiceResponse<UserClient>> GetUser(int id);
    Task<ServiceResponse<UserClient>> Login(LoginRequest loginRequest);
    Task<ServiceResponse<UserClient>> Register(RegisterRequest registerRequest);
}