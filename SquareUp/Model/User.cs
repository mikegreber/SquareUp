using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;

namespace SquareUp.Model;

[ObservableObject]
public partial class User : IUser
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _image = "dotnet_bot.png";

    [ObservableProperty]
    private string _email = string.Empty;
}

public partial class UserClient : User, IUserClient<FullyObservableCollection<GroupClient>, GroupClient>
{
    public int Id { get; set; }

    [ObservableProperty]
    private FullyObservableCollection<GroupClient> _groups = new();
}