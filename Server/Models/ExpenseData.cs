using SquareUp.Shared.Models;

namespace SquareUp.Server.Models;

public class ExpenseData
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public SplitType Type { get; set; }
    public UserData User { get; set; } = new();

    public static implicit operator Expense(ExpenseData data) => new(data);
}

public class Expense : IExpense<User>
{
    public Expense(ExpenseData data)
    {
        Id = data.Id;
        Name = data.Name;
        Amount = data.Amount;
        Type = data.Type;
        User = data.User;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public SplitType Type { get; set; }
    public User User { get; set; }
}