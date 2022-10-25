using System.Net.Http.Json;
using SquareUp.Model;
using SquareUp.Services.Transactions;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using static SquareUp.Shared.Endpoints.Groups;

namespace SquareUp.Services.Groups;

public class GroupService : IGroupService
{
    private readonly HttpClient _http;
    public event IGroupService.OnUserAddedDelegate OnUserAdded;
    public event IGroupService.OnGroupAddedDelegate OnGroupAdded;
    public event IGroupService.OnGroupChangedDelegate OnGroupChanged;
    public event IGroupService.OnGroupDeleteDelegate OnGroupDeleted;
    public event Action OnEdited;

    public GroupService(HttpClient http)
    {
        _http = http;
    }
    public void NotifyEdited() => OnEdited?.Invoke();

    public async Task<ServiceResponse<FullyObservableCollection<ObservableGroupInfo>>> GetGroups(int userId)
    {
        return await _http
            .GetAsync(GetGroupsByUserIdUri(userId))
            .ServiceResponse<FullyObservableCollection<ObservableGroupInfo>>();
    }

    public async Task<ServiceResponse<FullyObservableCollection<ObservableGroupInfo>>> GetUserGroupsInfo(int userId)
    {
        return await _http
            .GetAsync(GetGroupsInfoByUserIdUri(userId))
            .ServiceResponse<FullyObservableCollection<ObservableGroupInfo>>();
    }

    public async Task<ServiceResponse<ObservableGroup>> GetGroup(int id)
    {
        var result = await _http.GetFromJsonAsync<ServiceResponse<Group<List<UserBase>, List<Participant>, List<TransactionBase>>>>(GetGroupByIdUri(id));
        if (!result.Success) return new ServiceResponse<ObservableGroup>(message: result.Message);
        
        return new ServiceResponse<ObservableGroup>(result.Data, message: result.Message);
    }

    public async Task<ServiceResponse<ObservableGroupInfo>> CreateGroup(GroupBase group)
    {
        var response = await _http
            .PostAsJsonAsync(PostAddGroupUri, group)
            .ServiceResponse<ObservableGroupInfo>();

        if (response.Success) OnGroupAdded?.Invoke(response.Data);

        return response;
    }

    public async Task<ServiceResponse<ObservableUser>> AddUser(AddUserRequest request)
    {
        var response = await _http
            .PostAsJsonAsync(PostAddUserUri, request)
            .ServiceResponse<ObservableUser>();

        if (response.Success)
        {
            OnUserAdded?.Invoke(response.Data, request.GroupId);
        }

        return response;
    }

    public async Task<ServiceResponse<ObservableGroup>> DeleteGroup(ObservableGroup group)
    {
        var response = await _http
            .DeleteAsync(DeleteGroupUri(group.Id))
            .ServiceResponse<int>();

        if (response.Success) OnGroupDeleted?.Invoke(group);

        return new ServiceResponse<ObservableGroup>(group, response.Message);
    }

    public async Task<ServiceResponse<GroupInfo>> UpdateGroup(GroupRequest request)
    {
        return await _http
            .PutAsJsonAsync(PutEditGroupUri, request)
            .ServiceResponse<GroupInfo>();
    }

    public async Task<ServiceResponse<ObservableParticipant>> AddParticipant(AddParticipantRequest request)
    {
        return await _http
            .PostAsJsonAsync(PostAddParticipantUri, request)
            .ServiceResponse<ObservableParticipant>();
    }

    public async Task<ServiceResponse<ObservableParticipant>> InviteParticipant(InviteParticipantRequest request)
    {
        return await _http
            .PostAsJsonAsync(PostInviteParticipantUri, request)
            .ServiceResponse<ObservableParticipant>();
    }
}