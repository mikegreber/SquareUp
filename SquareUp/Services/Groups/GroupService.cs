using System.Net.Http.Json;
using System.Text.Json;
using SquareUp.Model;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Groups;
namespace SquareUp.Services.Groups;

public class GroupService : IGroupService
{
    private readonly HttpClient _http;

    public GroupService(HttpClient http) => _http = http;

    public Task<List<Group>> GetGroups(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResponse<GroupClient>> GetGroup(int id)
    {
        return await _http.GetFromJsonAsync<ServiceResponse<GroupClient>>(GetGroupByIdUri(id)) ?? new (message: "Request failed.");
    }

    public async Task<ServiceResponse<GroupClient>> AddGroup(AddGroupRequest request)
    {
        var response = await _http.PostAsJsonAsync(PostAddGroupUri, request);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<GroupClient>>() ?? new (message: "Request failed.");
    }

    public async Task<ServiceResponse<Expense>> AddExpense(AddExpenseRequest request)
    {
        var response = await _http.PostAsJsonAsync(PostAddExpenseUri, request);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<Expense>>() ?? new (message: "Request failed.");
    }

    public async Task<ServiceResponse<User>> AddUser(AddUserRequest request)
    {
        var response = await _http.PostAsJsonAsync(PostAddUserUri, request);
        return await response.Content.ReadFromJsonAsync<ServiceResponse<User>>() ?? new(message: "Request failed.");
    }
}