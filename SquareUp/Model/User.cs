using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;

namespace SquareUp.Model;

[ObservableObject]
public partial class ObservableUserBase : IUserBase, IUpdateable<IUserBase>
{
    public ObservableUserBase() { }

    public ObservableUserBase(IUserBase user)
    {
        Id = user.Id;
        Name = user.Name;
        Image = user.Image;
        Email = user.Email;
    }

    public int Id { get; set; }

    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _image = "dotnet_bot.png";

    [ObservableProperty]
    private string _email = string.Empty;

    public void Update(IUserBase obj)
    {
        Name = obj.Name;
        Image = obj.Image;
        Email = obj.Email;
    }
}

public partial class ObservableUser : ObservableUserBase, IUpdateable<ObservableUser>
{
    [ObservableProperty]
    private FullyObservableCollection<ObservableGroup> _groups = new();

    public void Update(ObservableUser obj)
    {
        base.Update(obj);
        Groups = obj.Groups;
    }
}