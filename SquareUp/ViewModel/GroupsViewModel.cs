using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.Services.Session;
using SquareUp.Shared.Types;
using SquareUp.View;

namespace SquareUp.ViewModel;

public partial class GroupsViewModel : BaseViewModel
{
    [ObservableProperty]
    private string _message = string.Empty;

    public GroupsViewModel(ISessionData session) : base(session)
    { }

    public override async Task OnBackButtonClicked()
    {
        await base.OnBackButtonClicked();
        await Session.SignOut();
    }

    public override Func<Task> OnActionButtonClicked { get; set; } = async () =>
    {
        await GroupDetailsPage.OpenAsync(new ObservableGroup { Color = ThemeBase.GroupColors.First() }, PageMode.Create, "Create Group");
    };

    [RelayCommand]
    public async Task Refresh()
    {
        await Session.GetUserGroups(refresh: true);
    }

    [RelayCommand]
    public async Task OnGroupTap(ObservableGroupInfo group)
    {
        await GroupPage.OpenAsync(group);
    }
}

public class GroupInfoList : GroupedFullyObservableCollection<DateTime, ObservableGroupInfo>
{
    public GroupInfoList() : base(
        compareGroups: (time, group) => group.Date.Date.CompareTo(time),
        compareItems: (e1, e2) => e2.Date.CompareTo(e1.Date),
        getGroupKey: transaction => transaction.Date.Date)
    { }

    public GroupInfoList(IEnumerable<ObservableGroupInfo> groups) : base(
        input: groups,
        compareGroups: (time, group) => group.Date.Date.CompareTo(time),
        compareItems: (e1, e2) => e2.Date.CompareTo(e1.Date),
        getGroupKey: transaction => transaction.Date.Date)
    { }
}