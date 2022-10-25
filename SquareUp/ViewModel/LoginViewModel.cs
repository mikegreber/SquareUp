using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Services.Session;
using SquareUp.Shared.Requests;
using SquareUp.View;

namespace SquareUp.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty] 
    private LoginRequest _loginRequest = new();

    [ObservableProperty] 
    private string _message = string.Empty;

    public LoginViewModel(ISessionData session) : base(session)
    { }

    [RelayCommand]
    public async Task Login()
    {
        Message = "Login in...";

        var response = await Session.Login(LoginRequest);
        
        if (response.Success)
        {
            await Session.GetUserGroups();
            await GroupsPage.OpenAsync();
        }
        else
        {
            Message = response.Message;
        }
    }

    [RelayCommand]
    public async Task Register()
    {
        await RegisterPage.OpenAsync();
    }

}