using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Model;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using SquareUp.ViewModel;

namespace SquareUp.Services.Session;

public interface ISessionData
{
    public ObservableUserBase User { get; set; }
    public GroupInfoList Groups { get; set; }
    public ObservableGroup Group { get; set; }
    public FullyObservableCollection<Debt> Debts { get; set; }
    public FullyObservableCollection<Settlement> Settlements { get; set; }
    public bool IsLoading { get; set; }

    Task<ServiceResponse<ObservableUserBase>> Login(LoginRequest request);
    Task<ServiceResponse<ObservableUserBase>> Register(RegisterRequest request);
    Task<ServiceResponse<GroupInfoList>> GetUserGroups(bool refresh = false);
    Task<ServiceResponse<ObservableGroup>> GetGroup(int id, bool refresh = false);
    Task<ServiceResponse<ObservableGroupInfo>> CreateGroup(GroupBase group);
    Task<ServiceResponse<GroupInfo>> UpdateGroup(GroupRequest request);
    Task<ServiceResponse<ObservableGroup>> DeleteGroup(ObservableGroup group);
    Task<ServiceResponse<ObservableTransaction>> CreateTransaction(ObservableTransaction transaction);
    Task<ServiceResponse<ObservableTransaction>> UpdateTransaction(ObservableTransaction transaction);
    Task<ServiceResponse<int>> DeleteTransaction(ObservableTransaction transaction);
    Task<ServiceResponse<ObservableParticipant>> AddParticipant(AddParticipantRequest request);
    Task<ServiceResponse<ObservableParticipant>> InviteParticipant(InviteParticipantRequest request);
    Task SignOut();
}