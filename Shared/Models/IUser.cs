namespace SquareUp.Shared.Models;

public interface IUser
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Email { get; set; }
}

public interface IUserClient<TGroupCollection, TGroup> : IUser where TGroupCollection : ICollection<TGroup>
{
    public int Id { get; set; }
    public TGroupCollection Groups { get; set; }
}