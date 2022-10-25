using System.Net.Http.Json;
using SquareUp.Model;
using SquareUp.Services.Transactions;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Users;
namespace SquareUp.Services.Users;

public class UserService : IUserService
{
    private readonly HttpClient _http;

    private ObservableUserBase? _user;

    public UserService(HttpClient http)
    {
        _http = http;
    }

    public ObservableUserBase? GetCurrentUser() => _user;

    public async Task<ServiceResponse<ObservableUserBase>> Login(LoginRequest request)
    {
        var response =  await _http
            .PostAsJsonAsync(PostLoginUri, request, true)
            .ServiceResponse<ObservableUserBase>();

        if (response.Success) _user = response.Data;
        
        return response;

        //return await _http.PostServiceCallAsJsonAsync<ObservableUserBase>(PostLoginUri, request);
        //return await ServiceCall<ObservableUserBase>(async () => await _http.PostAsJsonAsync(PostLoginUri, request));

        // try
        // {
        //     
        //
        //     var r = await _http
        //         .PostAsJsonAsync(PostLoginUri, request);
        //     if (r.IsSuccessStatusCode)
        //     {
        //         var response = await r.ServiceResponse<ObservableUserBase>();
        //
        //         if (response.Success) _user = response.Data;
        //
        //         return response;
        //     }
        //     
        // }
        // catch (Exception)
        // {
        //     
        // }
        //
        // return new ServiceResponse<ObservableUserBase>(message: "Error");
        // if (response.Success) _user = response.Data;
        //
        // return response;
    }

    public async Task<ServiceResponse<ObservableUserBase>> Register(RegisterRequest request)
    {

        var response = await _http
            .PostAsJsonAsync(PostRegisterUri, request)
            .ServiceResponse<ObservableUserBase>();

        if (response.Success) _user = response.Data;

        return response;
    }
}