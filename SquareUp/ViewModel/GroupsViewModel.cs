using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Services.Groups;
using SquareUp.Services.Users;
using SquareUp.Shared.Requests;
using SquareUp.View;

namespace SquareUp.ViewModel;

public partial class GroupsViewModel : BaseViewModel
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    [ObservableProperty]
    private UserClient _user = new();

    [ObservableProperty]
    private string _message = string.Empty;

    public GroupsViewModel(IUserService userService, IGroupService groupService)
    {
        _userService = userService;
        _groupService = groupService;

        GetData();
    }
    
    public async void GetData() => User = _userService.GetCurrentUser();
    public async void CreateGroup(string name)
    {
        var result = await _groupService.AddGroup(new AddGroupRequest { Name = name, UserId = User.Id });
        if (!result.Success) return;

        User.Groups.Insert(0, result.Data);
        await OnGroupTap(User.Groups.First());
    }

    public async Task OnGroupTap(GroupClient group)
    {
        await Shell.Current.GoToAsync(nameof(GroupPage), new Dictionary<string, object> { [nameof(group.Id)] = group.Id });
    }
}
