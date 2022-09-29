using SquareUp.Shared.Models;
using System.Linq;

namespace SquareUp.Server.Models;

public class UserData
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = "dotnet_bot.png";
    public string Email { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = { };
    public byte[] PasswordSalt { get; set; } = { };
    public List<GroupData> Groups { get; set; } = new();

    public static implicit operator UserClient(UserData data) => new(data);

    public static implicit operator User(UserData data) => new(data);
}


public class User : IUser
{
    public User(UserData data)
    {
        Name = data.Name;
        Image = data.Image;
        Email = data.Email;
    }
    public string Name { get; set; }
    public string Image { get; set; }
    public string Email { get; set; }
}

public class UserClient : User, IUserClient<List<GroupClient>, GroupClient>
{
    public UserClient(UserData data) : base(data)
    {
        Id = data.Id;
        Groups = data.Groups.Select(g => new GroupClient(g)).ToList();
    }

    public int Id { get; set; }
    public List<GroupClient> Groups { get; set; }
}