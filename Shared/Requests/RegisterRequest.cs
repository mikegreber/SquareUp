using CommunityToolkit.Mvvm.ComponentModel;

namespace SquareUp.Shared.Requests;

public partial class RegisterRequest : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _email = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _confirmPassword = string.Empty;
}