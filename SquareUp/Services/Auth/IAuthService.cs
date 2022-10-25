using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Auth
{
    public interface IAuthService
    {
        Task<ServiceResponse<ObservableUserBase>> Login(LoginRequest loginRequest);
        Task<ServiceResponse<ObservableUserBase>> Register(RegisterRequest registerRequest);
    }
}
