using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Shared.Requests;

namespace SquareUp.Controls
{
    public sealed class ExpensePopup : Popup
    {
        public static readonly BindableProperty AddExpenseRequestProperty = BindableProperty.Create(
            propertyName: nameof(AddExpenseRequest),
            returnType: typeof(AddExpenseRequest),
            declaringType: typeof(ExpensePopup),
            defaultValue: new AddExpenseRequest(),
            defaultBindingMode: BindingMode.TwoWay
        );

        public AddExpenseRequest AddExpenseRequest
        {
            get => (AddExpenseRequest)GetValue(AddExpenseRequestProperty);
            set => SetValue(AddExpenseRequestProperty, value);
        }


        public ExpensePopup()
        {
            CanBeDismissedByTappingOutsideOfPopup = true;
            BindingContext = this;
            
            Content = new VerticalStackLayout
            {
                BindingContext = this,
                Children =
                {
                    new Label
                    {
                        Text = "This is a very important message!"
                    },
                    new Entry()
                        .Bind(Entry.TextProperty, "AddExpenseRequest.Expense.Name"),
                    new Entry()
                        .Bind(Entry.TextProperty, "AddExpenseRequest.Expense.Amount"),
                    // new Label()
                    //     .Bind(Label.TextProperty, "AddExpenseRequest.Expense.Name"),
                    // new Label()
                    //     .Bind(Label.TextProperty, "AddExpenseRequest.Expense.Amount"),
                    new Button { Command = new Command(()=>Close(AddExpenseRequest)), }
                        .Text("Ok"),
                    new Button { Command = new Command(Close), }
                        .Text("Cancel"),
                }
            };
        }
    }
}
