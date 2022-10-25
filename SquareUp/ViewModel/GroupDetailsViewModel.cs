using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.Services.Session;
using SquareUp.Shared.Requests;
using SquareUp.View;
using IQueryAttributable = Microsoft.Maui.Controls.IQueryAttributable;

namespace SquareUp.ViewModel;

public partial class GroupDetailsViewModel : BaseViewModel, IQueryAttributable
{
    [ObservableProperty] 
    private ObservableGroup _group = new();

    [ObservableProperty] 
    private PageMode _mode;

    [ObservableProperty] 
    private string _title = string.Empty;

    [ObservableProperty]
    private List<string> _groupColors = ThemeBase.GroupColors;

    [ObservableProperty]
    private bool _isValidState;

    public GroupDetailsViewModel(ISessionData session) : base(session)
    {
        AnimateBackTransitions = false;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Delay(1);

        Title = (string)query[nameof(Title)];
        Mode = (PageMode)query[nameof(Mode)];
        Group = (ObservableGroup)query[nameof(Group)];

        // TODO should call the validator
        IsValidState = Group.Name.Length > 2;
    }

    public static Dictionary<string, object> Params(ObservableGroup group, PageMode mode, string title = "Group") => new()
    {
        [nameof(Title)] = title,
        [nameof(Group)] = group,
        [nameof(Mode)] = mode
    };

    [RelayCommand]
    private async Task AddParticipant()
    {
        // TODO use new view instead of prompt
        var prompt = await Page.DisplayPromptAsync("Add participant", "Enter name for new participant.");
        if (prompt == "Cancel") return;

        var response = await Session.AddParticipant(new() { GroupId = Group.Id, Name = prompt });
        await Page.DisplayAlert("Response", response.Message, "Ok");
    }

    [RelayCommand]
    private async Task InviteParticipant(ObservableParticipant participant)
    {
        // TODO use new view instead of prompt
        var email = await Page.DisplayPromptAsync("Invite user", "Enter email address");
        if (email == null) return;

        var response = await Session.InviteParticipant(new() {ParticipantId = participant.Id, GroupId = Group.Id, UserEmail = email });
        await Page.DisplayAlert("Response", response.Message, "Ok");
    }

    [RelayCommand]
    private async Task CreateGroup()
    {
        var result = await Session.CreateGroup(Group);
        await Shell.Current.GoToAsync("..", false);
        if (result.Success)
        {
            await GroupPage.OpenAsync(result.Data);
        }
    }

    [RelayCommand]
    private async Task UpdateGroup()
    {
        await Session.UpdateGroup(new GroupRequest{ GroupId = Group.Id, Name = Group.Name, Color = Group.Color });
        await Shell.Current.GoToAsync("..", false);
    }

    [RelayCommand]
    private async Task TapDeleteGroup()
    {
        var confirm = await Page.DisplayAlert($"Delete group?",
            $"Are you sure you want to delete the group {Group.Name}? This cannot be undone.", "Delete", "Cancel");
        if (confirm)
        {
            await Session.DeleteGroup(Group);
            await Shell.Current.GoToAsync("../..", false);
        }
    }

    
}

