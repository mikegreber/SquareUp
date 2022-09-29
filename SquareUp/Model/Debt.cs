using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;

namespace SquareUp.Model;
public partial class Debt : ObservableObject
{
    [ObservableProperty]
    private User _user = new();

    [ObservableProperty]
    private decimal _total;

    [ObservableProperty]
    private Dictionary<SplitType, decimal> _amounts = new()
    { 
        [SplitType.Split] = 0, 
        [SplitType.Proportional] = 0,
        [SplitType.Income] = 0,
    };

    public void Calculate(IEnumerable<Expense> expenses)
    {
        foreach (var key in Amounts.Keys)
            Amounts[key] = 0;
        
        foreach (var expense in expenses.Where(e => e.User.Name == User.Name))
            Amounts[expense.Type] += expense.Amount;
        
        Total = Amounts.Sum(k => k.Value);
    }
}
