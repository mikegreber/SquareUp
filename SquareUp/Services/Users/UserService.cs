using System.Net.Http.Json;
using SquareUp.Model;
using SquareUp.Shared;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Users;
namespace SquareUp.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _http;

    private UserClient? _user;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public UserClient? GetCurrentUser() => _user;

    public async Task<ServiceResponse<UserClient>> Login(LoginRequest request)
    {
        try
        {
            var result = await _http.PostAsJsonAsync(PostLoginUri, request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<UserClient>>() ??
                           new(message: "Request failed.");
            if (response.Success) _user = response.Data;
            return response;
        }
        catch (Exception e)
        {
            return new(message: $"Request failed. {e.Message}");
        }
        
    }

    public async Task<ServiceResponse<UserClient>> Register(RegisterRequest request)
    {
        try
        {
            var result = await _http.PostAsJsonAsync(PostRegisterUri, request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<UserClient>>() ?? new(message: "Request failed.");
            if (response.Success) _user = response.Data;
            return response;
        }
        catch (Exception e)
        {
            return new(message: $"Request failed. {e.Message}");
        }
    }
}