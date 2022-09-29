using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Layouts;
using SquareUp.Model;
using SquareUp.Shared;
using SquareUp.Shared.Models;
using SquareUp.ViewModel;

namespace SquareUp.View;

[QueryProperty("Expense", "Expense")]
public class EditExpensePage : BaseContentPage<MainViewModel>
{
    public EditExpensePage(MainViewModel mainViewModel) : base(mainViewModel)
    {
        
    }

    protected override void Build()
    {
        Title = "Edit Expense";
        Content = new VerticalStackLayout
        {
            BindingContext = BindingContext.Expense,
            Padding = 15,
            Children = {

                new FlexLayout()
                {
                    BindingContext = BindingContext.Expense,
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
                            BindingContext = BindingContext.Expense,
                            Children =
                            {
                                new Label().Text("Person"),
                                new Picker
                                {

                                    ItemsSource = BindingContext.People,
                                    ItemDisplayBinding = new Binding("Name")

                                }
                                    // .Bind(Picker.SelectedItemProperty, nameof(BindingContext.Expense.Person)),
                            }
                        }.Grow(1),

                        new BoxView()
                        {
                            WidthRequest = 10,
                        },

                        new VerticalStackLayout
                        {
                            BindingContext = BindingContext.Expense,

                            Children =
                            {
                                new Label()
                                    .Text("Type"),
                                new Picker()
                                {
                                    ItemsSource = Enum.GetValues(typeof(SplitType)),
                                }.Bind(Picker.SelectedItemProperty, nameof(BindingContext.Expense.Type)),
                            }
                        }.Grow(1),
                    }
                },

                new BoxView().Height(10),

                new Label().Text("Expense Title"),
                new Entry()
                    .Placeholder("Name")
                    .Bind(Entry.TextProperty, nameof(BindingContext.Expense.Name)),

                new BoxView().Height(10),

                new Label().Text("Amount"),
                new Entry()
                    .Placeholder("Name")
                    .Bind(Entry.TextProperty, nameof(BindingContext.Expense.Amount)),

                // new BoxView().Height(10),
                // new Button()
                //     .Text("Add")
                //     .BindCommand(nameof(BindingContext.AddExpenseCommand)),
            }
        };
    }
}