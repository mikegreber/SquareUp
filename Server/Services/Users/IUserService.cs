using SquareUp.Server.Models;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Users;

public interface IUserService
{
    Task<ServiceResponse<User>> GetCurrentUser();
    Task<ServiceResponse<List<User>>> GetUsers();
    Task<ServiceResponse<User>> GetUser(int id);
    Task<ServiceResponse<UserBase>> Login(LoginRequest loginRequest);
    Task<ServiceResponse<User>> Register(RegisterRequest registerRequest);
}