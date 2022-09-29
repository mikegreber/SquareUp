
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Shared;
using SquareUp.Shared.Models;

namespace SquareUp.ViewModel;

public partial class ExpenseViewModel : BaseViewModel
{
    private readonly MainViewModel _vm;

    public ExpenseViewModel(MainViewModel vm)
    {
        _vm = vm;
    }

    [ObservableProperty] 
    public Expense expense;

    public List<User> People => _vm.People;
    // protected override void ReadQueryAttributes(IDictionary<string, object> query)
    // {
    //     Expense = (Expense)query[nameof(Expense)];
    //     // People = (FullyObservableCollection<Person>)query[nameof(People)];
    // }

    [RelayCommand]
    public async void AddExpense()
    {
        MessagingCenter.Send(expense, "AddExpense");
        await Shell.Current.GoToAsync("..");
    }

    

}