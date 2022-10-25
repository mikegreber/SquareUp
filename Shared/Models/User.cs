using System.Collections;

namespace SquareUp.Shared.Models;

public interface IId
{
    public int Id { get; set; }
}

public interface IUpdateable<in T>
{
    void Update(T obj);
}


public interface IUserBase : IId
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Email { get; set; }
}

public interface IUser<TGroupCollection> : IUserBase where TGroupCollection : ICollection, new()
{
    public TGroupCollection Groups { get; set; }
}

public class UserBase : IUserBase
{
    public UserBase()
    {
        
    }

    public UserBase(IUserBase user)
    {
        Id = user.Id;
        Name = user.Name;
        Image = user.Image;
        Email = user.Email;
        Image = user.Image;
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}



public class User<TGroupCollection> : UserBase , IUser<TGroupCollection>
    where TGroupCollection : ICollection, new()
{
    public User() { }
    public User(IUserBase user) : base(user) { }
    public TGroupCollection Groups { get; set; } = new();
}