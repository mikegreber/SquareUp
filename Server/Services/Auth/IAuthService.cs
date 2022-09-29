using SquareUp.Shared.Types;
using SquareUp.Server.Models;

namespace SquareUp.Server.Services.Auth;

public interface IAuthService
{
    Task<ServiceResponse<UserClient>> Register(UserData user, string password);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
    int GetUserId();
    string GetUserEmail();
    Task<UserData> GetUserByEmail(string email);
    bool IsAdmin();

}