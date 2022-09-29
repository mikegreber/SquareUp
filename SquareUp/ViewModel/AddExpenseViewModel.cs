using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareUp.Model;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Shared;
using SquareUp.Shared.Models;

namespace SquareUp.ViewModel
{
    public partial class AddExpenseViewModel : BaseViewModel
    {
        private readonly MainViewModel _vm;

        public AddExpenseViewModel(MainViewModel vm)
        {
            _vm = vm;
        }

        [ObservableProperty]
        private string title = string.Empty;

        [ObservableProperty]
        private decimal amount;

        [ObservableProperty] 
        private SplitType type;

        [ObservableProperty]
        private User person = new User();

        [RelayCommand]
        public async Task AddExpense()
        {
            _vm.AddExpense(Person, new Expense { Name = Title, Amount = Amount, Type = type });
            
            await Shell.Current.GoToAsync("..");
        }
    }
}
