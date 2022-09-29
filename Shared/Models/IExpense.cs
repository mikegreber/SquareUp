namespace SquareUp.Shared.Models;

public interface IExpense<TUser>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public SplitType Type { get; set; }
    public TUser User { get; set; }
}

public enum SplitType
{
    Split, Proportional, Income
}