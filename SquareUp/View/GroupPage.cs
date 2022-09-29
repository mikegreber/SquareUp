using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class GroupPage : BaseContentPage<GroupViewModel>
{
    public GroupPage(GroupViewModel viewModel) : base(viewModel)
    {
        // Title = "Group";
    }

    protected override void Build()
    {
        // Title = "Square Up" + Navigation.NavigationStack.Count;

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
                            new RefreshView
                                {
                                    Content = new Label().Text("Refresh"),
                                }
                                
                                .Bind(RefreshView.IsRefreshingProperty, nameof(BindingContext.IsRefreshing))
                                .Bind(RefreshView.CommandProperty, nameof(BindingContext.PullToRefreshCommand)),
                                
                            new DebtCardTemplate.DebtCard().Bind(DebtCardTemplate.DebtCard.DebtsProperty, "Debts"),

                            new Label()
                                .Text("People")
                                .Margin(10, 0)
                                .Font(size: 18, bold: true),
                            new CollectionView()
                                .Bind(ItemsView.ItemsSourceProperty, "Group.Users")
                                .ItemTemplate(new UserCardTemplate(BindingContext.OnUserTap)),
                                

                            new Label()
                                .Text("Expenses")
                                .Margin(10, 0)
                                .Font(size: 18, bold: true),
                            new CollectionView()
                                .Bind(ItemsView.ItemsSourceProperty, "Group.Expenses")
                                .ItemTemplate(new ExpenseCardTemplate(BindingContext.OnExpenseTap))

                        }
                    }
                }.LayoutFlags(AbsoluteLayoutFlags.All).LayoutBounds(0, 0, 1, 1),


                new Button
                {
                    Padding = 0,
                    Margin = 16,
                    Text = "+",
                    BackgroundColor = Colors.DeepPink,
                    BindingContext = this,
                    CornerRadius = 60, WidthRequest = 60, HeightRequest = 60, Command = new Command(OpenPopup)
                }.Font(size: 28).LayoutFlags(AbsoluteLayoutFlags.PositionProportional).LayoutBounds(1, 1)
            }
        };
    }

    public async void OpenPopup()
    {
        var action = await DisplayActionSheet("Create new", "Cancel", null, "Person", "Expense");
        switch (action)
        {
            case "Person":
                await BindingContext.AddPerson(this);
                break;
            case "Expense":
                await BindingContext.AddExpense(this);
                break;
        }

        async void OnOkClicked(object obj)
        {
            await DisplayAlert("Title", obj.ToString(), "Cancel");
        }
    }
}