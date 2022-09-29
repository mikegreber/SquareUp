using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<UserClient>> Login(LoginRequest loginRequest);
        Task<ServiceResponse<UserClient>> Register(RegisterRequest registerRequest);
    }
}
