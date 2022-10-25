using System.Net.Http.Json;
using SquareUp.Model;
using SquareUp.Services.Transactions;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Users;

namespace SquareUp.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ServiceResponse<ObservableUserBase>> Login(LoginRequest loginRequest)
    {
        return await _http
            .PostAsJsonAsync(PostLoginUri, loginRequest)
            .ServiceResponse<ObservableUserBase>();
    }

    public async Task<ServiceResponse<ObservableUserBase>> Register(RegisterRequest request)
    {
        return await _http
            .PostAsJsonAsync(PostRegisterUri, request)
            .ServiceResponse<ObservableUserBase>();
    }
}