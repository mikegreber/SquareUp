using System.Collections.ObjectModel;
using System.ComponentModel;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using SquareUp.Model;
using SquareUp.Shared;
using SquareUp.Shared.Models;
using SquareUp.View;
using SquareUp.ViewModel;

namespace SquareUp.Controls;


public partial class DebtCard : ContentView, IDisposable
{
    private readonly MainViewModel _vm;

    public static readonly BindableProperty DebtProperty = BindableProperty.Create(
        propertyName: nameof(Debt),
        returnType: typeof(decimal),
        declaringType: typeof(DebtCard),
        defaultBindingMode: BindingMode.OneWay
    );
    public decimal Debt { get => (decimal) GetValue(DebtProperty); set => SetValue(DebtProperty, value); }


    public static readonly BindableProperty ExpensesProperty = BindableProperty.Create(
        propertyName: nameof(Expenses),
        returnType: typeof(ObservableCollection<Expense>),
        defaultValue: new ObservableCollection<Expense>(),
        declaringType: typeof(DebtCard)
    );

    public ObservableCollection<Expense> Expenses { get => (ObservableCollection<Expense>)GetValue(ExpensesProperty); set => SetValue(ExpensesProperty, value); }

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        propertyName: nameof(Text),
        returnType: typeof(string),
        declaringType: typeof(DebtCard),
        defaultValue: "default"
    );
    public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }


    public static readonly BindableProperty MainViewModelProperty = BindableProperty.Create(
        propertyName: nameof(Model),
        returnType: typeof(MainViewModel),
        declaringType: typeof(DebtCard)
    );
    public MainViewModel Model { get => (MainViewModel)GetValue(MainViewModelProperty); set => SetValue(MainViewModelProperty, value); }

    public DebtCard(MainViewModel vm)
    {
        vm.PropertyChanged += OnPropertyChanged;
        _vm = vm;
        BindingContext = vm;
        Content = new Frame
        {
            Padding = 10,
            Margin = 5,
            
            Content = new VerticalStackLayout
            {
                BindingContext = vm,
                
                Children =
                {
                    new Label().Text("Debts").Font(size: 24, bold:true),
                    new Label().Text("Debts").Font(size: 24, bold:true).Bind<Label, ObservableCollection<Expense>, string>(Label.TextProperty, nameof(vm.Expenses), convert: expenses => expenses.Sum(e=>e.Amount).ToString()),
                }
            },
        };
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Content = new Frame
        {
            Padding = 10,
            Margin = 5,

            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label()
                        .Text(e.PropertyName)
                        .Font(size: 12, bold:true),
                    new Label()
                        .Text("Debts")
                        .Font(size: 24, bold:true)
                        .Bind<Label, ObservableCollection<Expense>, string>(Label.TextProperty, "Expenses", convert: expenses => expenses.Sum(e=>e.Amount).ToString()),
                    // new Label()
                    //     .Text("Debts")
                    //     .Font(size: 24, bold:true)
                    //     .Bind<Label, ObservableCollection<User>, string>(Label.TextProperty, "People", convert: people => people.Sum(p=>p.Income).ToString()),
                }
            },
        };
    }

    public void Dispose()
    {
        _vm.PropertyChanged -= OnPropertyChanged;
    }
}