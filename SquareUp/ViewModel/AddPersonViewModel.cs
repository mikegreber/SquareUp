using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Shared.Models;

namespace SquareUp.ViewModel;

public partial class AddPersonViewModel : BaseViewModel
{
    private readonly MainViewModel _vm;

    public AddPersonViewModel(MainViewModel vm)
    {
        _vm = vm;
    }

    [ObservableProperty]
    private string _name;

    [RelayCommand]
    public async Task AddPerson()
    {
        _vm.People.Add(new User() { Name = Name });
        await Shell.Current.GoToAsync("..");
    }
}