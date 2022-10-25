using SquareUp.Server.Models;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Server.Services.Groups;

public interface IGroupService
{
    Task<ServiceResponse<List<Group>>> GetGroups();
    Task<ServiceResponse<List<GroupInfo>>> GetGroupsInfo();
    Task<ServiceResponse<List<GroupBase>>> GetUserGroups(HttpRequest request);
    Task<ServiceResponse<List<GroupInfo>>> GetUserGroupsInfo(HttpRequest request);
    Task<ServiceResponse<GroupInfo>> CreateGroup(HttpRequest request, GroupBase group);
    Task<ServiceResponse<Participant>> AddParticipant(HttpRequest request, AddParticipantRequest payload);
    Task<ServiceResponse<Participant>> InviteParticipant(HttpRequest request, InviteParticipantRequest payload);
    Task<ServiceResponse<User>> AddUser(HttpRequest request, AddUserRequest payload);
    Task<ServiceResponse<Group>> GetGroup(HttpRequest request, int id);
    Task<ServiceResponse<GroupInfo>> UpdateGroup(HttpRequest request, GroupRequest payload);
    Task<ServiceResponse<int>> DeleteGroup(HttpRequest request, int id);

    
}