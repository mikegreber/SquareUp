using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Users;

public interface IUserService
{
    UserClient? GetCurrentUser();
    Task<ServiceResponse<UserClient>> Login(LoginRequest request);
    Task<ServiceResponse<UserClient>> Register(RegisterRequest request);
}