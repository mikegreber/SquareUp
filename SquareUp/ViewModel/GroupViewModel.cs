using System.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Controls;
using SquareUp.Model;
using SquareUp.Services.Groups;
using SquareUp.Services.Users;
using SquareUp.Shared.Models;
using SquareUp.Shared.Requests;
using SquareUp.Shared.Types;
using SquareUp.View;
using IQueryAttributable = Microsoft.Maui.Controls.IQueryAttributable;

// using IQueryAttributable = SquareUp.Model.IQueryAttributable;

namespace SquareUp.ViewModel;

public partial class GroupViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IUserService _userService;
    private readonly IGroupService _groupService;

    [ObservableProperty]
    private GroupClient _group = new();

    [ObservableProperty] 
    private bool _isRefreshing = true;

    [ObservableProperty] 
    private FullyObservableCollection<Debt> _debts = new();

    public GroupViewModel(IUserService userService, IGroupService groupService)
    {
        _userService = userService;
        _groupService = groupService;
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.PropertyName == "Debts") return;

        Debts.Clear();
        foreach (var user in Group.Users)
        {
            var debt = new Debt{User = user};
            debt.Calculate(Group.Expenses);
            Debts.Add(debt);
        }
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var id = query[nameof(Group.Id)];
        
        if (id == null) return;

        var response = await _groupService.GetGroup((int) id);
        if (response.Success)
            Group = response.Data;

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task PullToRefresh()
    {
        Group.Expenses.Clear();
        Group.Users.Clear();

        try
        {
            var response = await _groupService.GetGroup(Group.Id);
            if (response.Success)
                Group = response.Data;
        }
        catch (Exception e)
        {

        }
        finally
        {
            IsRefreshing = false;
        }
    }

    public async Task AddPerson(string email)
    {
        var result = await _groupService.AddUser(new AddUserRequest { UserEmail = email, GroupId = Group.Id });
        if (result.Success)
        {
            Group.Users.Add(result.Data);
            await Application.Current!.MainPage!.DisplayAlert("Success", result.Message, "Ok");
        }
        else
        {
            await Application.Current!.MainPage!.DisplayAlert("Add Failed", result.Message, "Ok");
        }
    }

    public async Task AddPerson(Page page)
    {
        var email = await page.DisplayPromptAsync("Add Person", "Enter the users email address");
        if (email is "Cancel" or "") return;

        var result = await _groupService.AddUser(new AddUserRequest { UserEmail = email, GroupId = Group.Id });
        if (result.Success)
        {
            Group.Users.Add(result.Data);
            await Application.Current!.MainPage!.DisplayAlert("Success", result.Message, "Ok");
        }
        else
        {
            await Application.Current!.MainPage!.DisplayAlert("Add Failed", result.Message, "Ok");
        }
    }

    

    public async Task AddExpense(Page page)
    {
        var request = (AddExpenseRequest) await page.ShowPopupAsync(new ExpensePopup());
        if (request == null) return;

        request.GroupId = Group.Id;
        request.UserId = _userService.GetCurrentUser().Id;

        var result = await _groupService.AddExpense(request);
        if (result.Success)
        {
            Group.Expenses.Add(result.Data);
            await Application.Current!.MainPage!.DisplayAlert("Success", result.Message, "Ok");
        }
        else
        {
            await Application.Current!.MainPage!.DisplayAlert("Add Failed", result.Message, "Ok");
        }
    }

    public async Task OnExpenseTap(Expense expense)
    {
        await Application.Current!.MainPage!.DisplayAlert("Expense Tapped!", expense.Name, "Ok");
    }

    public async Task OnUserTap(User user)
    {
        await Application.Current!.MainPage!.DisplayAlert("User Tapped!", user.Name, "Ok");
    }
}

