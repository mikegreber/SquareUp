using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;

namespace SquareUp.Model;

[ObservableObject]
public partial class Expense : IExpense<User>
{
    public int Id { get; set; }
    [ObservableProperty] private string _name = string.Empty;

    [ObservableProperty] private decimal _amount;

    [ObservableProperty] private SplitType _type;
    public User User { get; set; }
}