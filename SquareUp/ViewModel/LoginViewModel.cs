using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Services.Users;
using SquareUp.Shared.Requests;
using SquareUp.View;

namespace SquareUp.ViewModel;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IUserService _userService;

    [ObservableProperty] 
    private LoginRequest _loginRequest = new();

    [ObservableProperty]
    private RegisterRequest _registerRequest = new();

    [ObservableProperty] 
    private string _message = string.Empty;

    public LoginViewModel(IUserService userService) => _userService = userService;

    [RelayCommand]
    public async Task Login()
    {
        var response = await _userService.Login(LoginRequest);
        Message = response.Message;
        if (response.Success) await Shell.Current.GoToAsync(nameof(GroupsPage));
    }

    [RelayCommand]
    public async Task Register()
    {
        var response = await _userService.Register(RegisterRequest);
        Message = response.Message;
        if (response.Success) await Shell.Current.GoToAsync(nameof(GroupsPage));
    }
}