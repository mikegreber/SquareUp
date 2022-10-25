using SquareUp.Shared.Models;

namespace SquareUp.Server.Models;

public class UserData : User<List<GroupData>>
{
    public byte[] PasswordHash { get; set; } = { };
    public byte[] PasswordSalt { get; set; } = { };
    
    public static implicit operator User(UserData data) => new(data);
}

public class User : User<List<GroupBase>>
{
    public User(IUserBase data) : base(data) { }
}