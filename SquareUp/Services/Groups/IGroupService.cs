using SquareUp.Model;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;

namespace SquareUp.Services.Groups;

public interface IGroupService
{
    Task<ServiceResponse<FullyObservableCollection<ObservableGroupInfo>>> GetGroups(int userId);
    Task<ServiceResponse<FullyObservableCollection<ObservableGroupInfo>>> GetUserGroupsInfo(int userId);
    Task<ServiceResponse<ObservableGroup>> GetGroup(int id);
    Task<ServiceResponse<ObservableGroupInfo>> CreateGroup(GroupBase group);
    Task<ServiceResponse<ObservableUser>> AddUser(AddUserRequest request);
    Task<ServiceResponse<ObservableGroup>> DeleteGroup(ObservableGroup group);
    Task<ServiceResponse<GroupInfo>> UpdateGroup(GroupRequest request);
    Task<ServiceResponse<ObservableParticipant>> AddParticipant(AddParticipantRequest request);
    Task<ServiceResponse<ObservableParticipant>> InviteParticipant(InviteParticipantRequest request);

    public delegate void OnUserAddedDelegate(ObservableUser user, int groupId);
    public event OnUserAddedDelegate OnUserAdded;

    public delegate void OnGroupAddedDelegate(ObservableGroupInfo group);
    public event OnGroupAddedDelegate OnGroupAdded;

    public delegate void OnGroupChangedDelegate(ObservableGroup group);
    public event OnGroupChangedDelegate OnGroupChanged;

    public delegate void OnGroupDeleteDelegate(ObservableGroup group);
    public event OnGroupDeleteDelegate OnGroupDeleted;

}