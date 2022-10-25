using System.Net.Http.Headers;
using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Model;
using SquareUp.Services.Groups;
using SquareUp.Services.Transactions;
using SquareUp.Services.Users;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using SquareUp.ViewModel;

namespace SquareUp.Services.Session;

public partial class SessionData : ObservableObject, ISessionData
{
    private readonly HttpClient _httpClient;
    public IUserService UserService { get; }
    public IGroupService GroupService { get; }
    public ITransactionService TransactionService { get; }

    [ObservableProperty]
    private ObservableUserBase? _user;

    [ObservableProperty]
    private GroupInfoList? _groups;

    [ObservableProperty]
    private ObservableGroup? _group;

    [ObservableProperty]
    private bool _isLoading;

    public SessionData(IUserService userService, IGroupService groupService, ITransactionService transactionService, HttpClient httpClient)
    {
        _httpClient = httpClient;
        UserService = userService;
        GroupService = groupService;
        TransactionService = transactionService;
    }

    

    public async Task<ServiceResponse<ObservableUserBase>> Login(LoginRequest request)
    {
        IsLoading = true;
        var response = await UserService.Login(request);
        if (response.Success)
        {
            User = response.Data;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Message);
        }

        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<ObservableUserBase>> Register(RegisterRequest request)
    {
        IsLoading = true;
        var response = await UserService.Register(request);
        if (response.Success)
        {
            User = response.Data;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Message);
        }

        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<GroupInfoList>> GetUserGroups(bool refresh = false)
    {
        if (refresh || Groups == null)
        {
            IsLoading = true;
            var response = await GroupService.GetUserGroupsInfo(User.Id);
            if (response.Success) Groups = new GroupInfoList(response.Data);
        }

        IsLoading = false;
        return new ServiceResponse<GroupInfoList>(Groups);
    }

    public async Task<ServiceResponse<ObservableGroup>> GetGroup(int id, bool refresh = false)
    {
        if (refresh || Group == null || Group.Id != id)
        {
            IsLoading = true;
            var response = await GroupService.GetGroup(id);
            if (response.Success) Group = response.Data;

            IsLoading = false;
            return response;
        }

        return new ServiceResponse<ObservableGroup>(Group);
    }

    public async Task<ServiceResponse<ObservableGroupInfo>> CreateGroup(GroupBase group)
    {
        IsLoading = true;
        var response = await GroupService.CreateGroup(group);
        if (response.Success) Groups?.Add(response.Data);

        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<GroupInfo>> UpdateGroup(GroupRequest request)
    {
        IsLoading = true;
        var response = await GroupService.UpdateGroup(request);
        if (!response.Success)
        {
            IsLoading = false;
            return response;
        }

        Group?.Update(response.Data);
        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<ObservableGroup>> DeleteGroup(ObservableGroup group)
    {
        IsLoading = true;
        var response = await GroupService.DeleteGroup(group);
        if (response.Success) Group = new();
        
        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<ObservableTransaction>> CreateTransaction(ObservableTransaction transaction)
    {
        IsLoading = true;
        var response = await TransactionService.Create(transaction, Group.Id);
        if (!response.Success)
        {
            IsLoading = false;
            return response;
        }

        Group.Add(response.Data);
        UpdateLastEditTime();
        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<ObservableTransaction>> UpdateTransaction(ObservableTransaction transaction)
    {
        IsLoading = true;
        var response = await TransactionService.Update(transaction);
        if (!response.Success)
        {
            IsLoading = false;
            return response;
        }

        Group?.Update(response.Data);
        UpdateLastEditTime();
        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<int>> DeleteTransaction(ObservableTransaction transaction)
    {
        IsLoading = true;
        var response = await TransactionService.Delete(transaction);
        if (!response.Success)
        {
            IsLoading = false;
            return response;
        }

        Group.Transactions.Delete(transaction);
        UpdateLastEditTime();
        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<ObservableParticipant>> AddParticipant(AddParticipantRequest request)
    {
        IsLoading = true;
        var response = await GroupService.AddParticipant(request);
        if (!response.Success)
        {
            IsLoading = false;
            return response;
        }

        Group.Participants.Add(response.Data);
        UpdateLastEditTime();
        IsLoading = false;
        return response;
    }

    public async Task<ServiceResponse<ObservableParticipant>> InviteParticipant(InviteParticipantRequest request)
    {
        IsLoading = true;
        var response = await GroupService.InviteParticipant(request);
        if (!response.Success)
        {
            IsLoading = false;
            return response;
        }

        var participant = Group.Participants.FirstOrDefault(p => p.Id == response.Data.Id);
        if (participant != null)
        {
            participant.UserId = response.Data.UserId;
            participant.Name = response.Data.Name;
        }
        UpdateLastEditTime();
        IsLoading = false;
        return response;
    }

    public async Task SignOut()
    {
        Group = null;
        Groups = null;
        User = null;
    }


    private void UpdateLastEditTime()
    {
        Group.LastEdit = DateTime.Now;
        Enumerable.SelectMany<ObservableCollectionGroup<DateTime, ObservableGroupInfo>, ObservableGroupInfo>(Groups, g => g).First(g => g.Id == Group.Id).LastEdit = DateTime.Now;
    }
}