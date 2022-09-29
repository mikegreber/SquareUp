using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Layouts;
using SquareUp.Model;
using SquareUp.Shared;
using SquareUp.Shared.Models;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class AddExpensePage : BaseContentPage<ExpenseViewModel>
{
    public AddExpensePage(ExpenseViewModel viewModel) : base(viewModel, true)
    {

    }

    protected override void Build()
    {
        Title = "Add Expense" + Shell.Current.Items.Count;
        Content = new VerticalStackLayout
        {
            Padding = 15,
            // BindingContext = BindingContext,
            Children = {

                new FlexLayout()
                {
                    // BindingContext = BindingContext,
                    Direction = FlexDirection.Row,
                    AlignItems = FlexAlignItems.Stretch,
                    AlignContent = FlexAlignContent.Stretch,
                    JustifyContent = FlexJustify.SpaceBetween,
                    Margin = 0,
                    HeightRequest = 50,
                    Padding = 0,

                    Children = {
                        new VerticalStackLayout()
                        {
                            // BindingContext = BindingContext,
                            Children =
                            {
                                new Label().Text("Person"),
                                new Picker
                                {
                                    ItemsSource = BindingContext.People,
                                    ItemDisplayBinding = new Binding("Name")

                                }.Bind(Picker.SelectedItemProperty, "Expense.Person"),
                            }
                        }.Grow(1),

                        new BoxView()
                        {
                            WidthRequest = 10,
                        },

                        new VerticalStackLayout
                        {
                            Children =
                            {
                                new Label()
                                    .Text("Type"),
                                new Picker()
                                {
                                    ItemsSource = Enum.GetValues(typeof(SplitType)),
                                }.Bind(Picker.SelectedItemProperty, "Expense.Type"),
                            }
                        }.Grow(1),
                    }
                },

                new BoxView().Height(10),

                new Label().Text("Expense Title"),
                new Entry()
                        // {BindingContext = BindingContext.Expense}
                    .Placeholder("Name")
                    .Bind(Entry.TextProperty, "Expense.Name"),

                new BoxView().Height(10),

                new Label().Text("Amount"),
                new Entry()
                    .Placeholder("Name")
                    .Bind(Entry.TextProperty, "Expense.Amount"),

                new BoxView().Height(10),
                new Button()
                    .Text("Add")
                    .BindCommand(nameof(BindingContext.AddExpenseCommand)),

                new Button()
                {
                    Command = new RelayCommand(Execute)
                }.Text("Test"),

                new Label().Bind(Label.TextProperty, "Expense.Name"),
                new Label().Bind(Label.TextProperty, "Expense.Amount"),
                new Label().Bind(Label.TextProperty, "Expense.Type"),
                new Label().Bind(Label.TextProperty, "Expense.Person.Name"),
            }
        };
    }

    private void Execute()
    {
        Build();
    }
}