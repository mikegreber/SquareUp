using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Services.Session;
using SquareUp.Shared.Requests;
using SquareUp.View;

namespace SquareUp.ViewModel;

public partial class RegisterViewModel : BaseViewModel
{
    [ObservableProperty]
    private RegisterRequest _registerRequest = new();

    [ObservableProperty]
    private string _message = string.Empty;

    public RegisterViewModel(ISessionData session) : base(session)
    { }

    [RelayCommand]
    public async Task Register()
    {
        var response = await Session.Register(RegisterRequest);
        if (response.Success)
        {
            await Session.GetUserGroups();
            await Shell.Current.GoToAsync($"../{nameof(GroupsPage)}");
        }
        else
        {
            Message = response.Message;
        }
    }
}