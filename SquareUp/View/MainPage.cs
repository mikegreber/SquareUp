using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.Model;
using SquareUp.ViewModel;


namespace SquareUp.View;

public class MainPage : BaseContentPage<MainViewModel>
{
    public MainPage(MainViewModel mainViewModel) : base(mainViewModel)
    {
        // Build();
    }

    protected override void Build()
    {
        Title = "Square Up" + Navigation.NavigationStack.Count;
        
        Content = new AbsoluteLayout
        {
            Children =
            {
                new ScrollView
                {
                    Content = new VerticalStackLayout
                    {
                        Spacing = 0,
                        Padding = 5,
                        BindingContext = BindingContext,
                        Children =
                        {
                            new Label()
                                .Text("Square Up")
                                .Font(size: 32)
                                .CenterHorizontal(),

                            new Label()
                                .Text("Square Up")
                                .Font(size: 32)
                                .CenterHorizontal()
                                .Bind<Label, decimal, string>
                                    (Label.TextProperty, nameof(BindingContext.Debt), convert: d => $"${d:0.00}"),

                            new Label()
                                {
                                    BindingContext = BindingContext,
                                }
                                .Text("Square Up")
                                .Font(size: 32)
                                .CenterHorizontal()
                                // .Bind<Label, ObservableCollection<User>, string>
                                //     (Label.TextProperty, nameof(BindingContext.People), convert: people => $"${people.Sum(p => p.Expenses.Sum(e => e.Amount)):0.00}")
                            ,

                            new DebtCard(BindingContext)
                            {
                                
                            },
                            new Label()
                                .Text("People")
                                .Margin(10, 0)
                                .Font(size: 18, bold: true),
                            new CollectionView()
                                .Row(2)
                                .ColumnSpan(2)
                                .ItemsSource(BindingContext.People)
                                .ItemTemplate(
                                    new DataTemplate(() =>
                                    {
                                        var item = new PersonListItem();
                                        item.Bind(PersonListItem.PersonProperty);
                                        return item;
                                    })
                                ),

                            new Label()
                                .Text("Square Up")
                                .Font(size: 32)
                                .CenterHorizontal()
                                // .Bind<Label, FullyObservableCollection<User>, string>
                                //     (Label.TextProperty, nameof(BindingContext.People), convert: people => $"$ {people.Sum(p=>p.Expenses.Sum(e=>e.Amount)):0.00}")
                            ,

                            new Label()
                                .Text("Expenses")
                                .Margin(10, 0)
                                .Font(size: 18, bold: true),
                            new CollectionView()
                                .Row(2)
                                .ColumnSpan(2)
                                .ItemsSource(BindingContext.Expenses)
                                .ItemTemplate(new ExpenseCardTemplate()),
                            

                            new Button()
                            {
                                Command = new Command(Test)
                            }.Text("Test"),
                            new Button()
                            {
                            Command = new Command(Test2)
                        }.Text("Test2")
                        }
                    }
                }.LayoutFlags(AbsoluteLayoutFlags.All).LayoutBounds(0,0,1,1),
                new Button {
                    Padding = 0,
                    Margin=16,
                    Text = "+",
                    BackgroundColor = Colors.DeepPink,
                    BindingContext = this,
                    CornerRadius = 60, WidthRequest = 60, HeightRequest = 60, Command = new Command(OpenPopup)
                }.Font(size: 28).LayoutFlags(AbsoluteLayoutFlags.PositionProportional).LayoutBounds(1,1)
            }
        };
    }

    public void Test()
    {
        var p = BindingContext.People.First();
        p.Name = p.Name == "Lorii" ? "Joe" : "Lorii";
        // BindingContext.AddExpense(BindingContext.People[0], new Expense{Name="New", Amount=143});
    }

    public void Test2()
    {
        Build();
    }

    public async void OpenPopup()
    {
        
        var action = await DisplayActionSheet("Create new", "Cancel", null, "Person", "Expense");
        switch (action)
        {
            case "Person":
                await BindingContext.GoToAddPersonPage();
                break;
            case "Expense":
                await BindingContext.GoToAddExpensePage();
                break;
        }
    }

    
}

public static class Extensions
{
    public static DataTemplate Bind(this DataTemplate dataTemplate, BindableProperty targetProperty, string path)
    {
        return dataTemplate.Bind(targetProperty, new Binding(path));
    }

    public static DataTemplate Bind(this DataTemplate dataTemplate, BindableProperty targetProperty,
        BindingBase binding)
    {
        dataTemplate.SetBinding(targetProperty, binding);
        return dataTemplate;
    }
}