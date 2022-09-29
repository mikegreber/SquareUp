using System.Net.Http.Json;
using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Users;
namespace SquareUp.Services.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;

    public AuthService(HttpClient http) => _http = http;

    public async Task<ServiceResponse<UserClient>> Login(LoginRequest loginRequest)
    {
        var response = await _http.PostAsJsonAsync(PostLoginUri, loginRequest);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<UserClient>>();
    }

    public async Task<ServiceResponse<UserClient>> Register(RegisterRequest request)
    {
        var result = await _http.PostAsJsonAsync(PostRegisterUri, request);
        var response = await result.Content.ReadFromJsonAsync<ServiceResponse<UserClient>>();
        //if (response.Data != null) _user = response.Data;
        return response;
    }
}