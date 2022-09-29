using SquareUp.Shared.Models;
using System.Text.RegularExpressions;

namespace SquareUp.Server.Models;

public class GroupData
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<UserData> Users { get; set; } = new();
    public List<ExpenseData> Expenses { get; set; } = new();

    public static implicit operator GroupClient(GroupData data) => new(data);
    public static implicit operator Group(GroupData data) => new(data);
}

public class Group : IGroup
{
    public Group(GroupData data)
    {
        Name = data.Name;
    }
    public string Name { get; set; }
}

public class GroupClient : Group, IGroupClient<List<User>, User, List<Expense>, Expense>
{
    public GroupClient(GroupData data) : base(data)
    {
        Id = data.Id;
        Users = data.Users.Select(u => new User(u)).ToList();
        Expenses = data.Expenses.Select(e => new Expense(e)).ToList();
    }

    public int Id { get; set; }
    public List<User> Users { get; set; }
    public List<Expense> Expenses { get; set; }
}