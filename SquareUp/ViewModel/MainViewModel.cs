using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.View;
using SquareUp.Services.Groups;
using SquareUp.Services.Navigation;
using SquareUp.Services.Users;
using SquareUp.Shared;
using SquareUp.Shared.Models;

namespace SquareUp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    private readonly IGroupService _userService;

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);
        Console.WriteLine("Property Changed!");
    }

    public MainViewModel(INavigationService navigationService, IGroupService userService)
    {
        _navigationService = navigationService;
        _userService = userService;

        // Expenses = expenseService.GetExpenses();
        // People = userService.()

        // People.OnElementChanged += async p =>
        // {
        //     Expenses.StopNotifications();
        //     foreach (var e in p.Expenses)
        //         Expenses.NotifyChange(e);
        //     Expenses.StartNotifications();
        // };
        //
        // Expenses.OnElementChanged += async (e) =>
        // {
        //     People.StopNotifications();
        //     People.NotifyChange(e.Person);
        //     People.StartNotifications();
        // };
        //
        // Expenses.OnChanged += async (sender, args) =>
        // {
        //     People.StopNotifications();
        //     foreach (var p in People) People.NotifyChange(p);
        //     People.StartNotifications();
        // };
        //
        // People.Add(new Person { Name="Lorii", Income=3982 });
        // People.Add(new Person { Name="Mike", Income=2438 });
        //
        // AddExpense(People[0], new Expense {Name="Credit Card", Amount=1834});
        // AddExpense(People[0], new Expense {Name="Rent", Amount=1300});
        // AddExpense(People[1], new Expense {Name="Phones", Amount=153});
        //
        // debt = Expenses.Sum(e => e.Amount);

        // MessagingCenter.Subscribe<Expense>(this, "AddExpense", AddNewExpense);
    }

    private async void AddNewExpense(Expense e)
    {
        await Task.Delay(2000);

        // AddExpense(People.First(p => p.Name == e.Person.Name), e);
        
        
        // if (e != null)
        // {
        //     Expenses.Add(e);
        // }
        // else
        // {BindingContext.People[0], new Expense{Name="New", Amount=143}
            // Expenses.Add(new Expense(){Name="Was Null", Person=People.First(p => p == person), Type=Expense.SplitType.Proportional, Amount=10});
        // }
    }

    public void AddExpense(User person, Expense expense)
    {
        var p = People.First(p => p == person);
        // expense.Person = p;
        // Expenses.Add(expense);
        // p.Expenses.Add(expense);
    }



    [ObservableProperty]
    private List<Expense> _expenses = new();

    [ObservableProperty]
    private List<User> _people = new();

    [ObservableProperty]
    private string newName = string.Empty;

    [ObservableProperty]
    private User person = new();

    [ObservableProperty]
    private Expense expense = new();

    [ObservableProperty]
    public decimal debt;

    [RelayCommand]
    public async Task GoToAddPersonPage() => await Shell.Current.GoToAsync(nameof(AddPersonPage));

    [RelayCommand]
    public async Task GoToAddExpensePage()
    {
        Dictionary<string, object> parameters = new()
        {
            // ["Expense"] = new Expense { Name = "Name", Person = new User() },
            ["People"] = People
        };

        await Shell.Current.GoToAsync(nameof(AddExpensePage), parameters);
    }

    public async void Calculate()
    {
        await Task.Delay(4);
        // Debt = People.Sum(p => p.Expenses.Sum(e => e.Amount));
    }

    public void Notify()
    {
    }
}