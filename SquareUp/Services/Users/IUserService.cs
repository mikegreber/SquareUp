using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Users;

public interface IUserService
{
    ObservableUserBase? GetCurrentUser();
    Task<ServiceResponse<ObservableUserBase>> Login(LoginRequest request);
    Task<ServiceResponse<ObservableUserBase>> Register(RegisterRequest request);
}