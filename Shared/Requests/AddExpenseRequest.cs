using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Shared.Models;

namespace SquareUp.Shared.Requests;

public partial class AddExpenseRequest
{
    public int UserId { get; set; }
    public int GroupId { get; set; }
    public Data Expense { get; set; } = new();


    [ObservableObject]
    public partial class Data
    {
        [ObservableProperty] private decimal _amount;
        [ObservableProperty] private string _name = string.Empty;
        [ObservableProperty] private SplitType _type;
    }
}