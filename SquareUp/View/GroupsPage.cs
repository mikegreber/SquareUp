using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.Model;
using SquareUp.Shared;
using SquareUp.Shared.Models;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class GroupsPage : BaseContentPage<GroupsViewModel>
{
    public GroupsPage(GroupsViewModel viewModel) : base(in viewModel) { }

    protected override void Build()
    {
        Title = "Square Up";
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
                            new Label
                                {
                                    BindingContext = BindingContext
                                }
                                .Text("Square Up")
                                .Font(size: 32)
                                .CenterHorizontal()
                                .Bind<Label, User, string>
                                    (Label.TextProperty, "User", convert: user => $"Hello {user.Name}!"),

                            new Label()
                                .Text("Groups")
                                .CenterHorizontal()
                                .Font(size: 18, bold: true),
                            new CollectionView()
                                .Bind(ItemsView.ItemsSourceProperty, "User.Groups")
                                .ItemTemplate(new GroupCardTemplate(BindingContext.OnGroupTap)),
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
                    CornerRadius = 60, 
                    WidthRequest = 60, 
                    HeightRequest = 60, 
                    Command = new RelayCommand(Add)
                }
                    .Font(size: 28)
                    .LayoutFlags(AbsoluteLayoutFlags.PositionProportional)
                    .LayoutBounds(1, 1)
            }
        };
    }

    private async void Add()
    {
        var result = await DisplayPromptAsync("Create new group", "Group name");
        if (result != "Cancel") BindingContext.CreateGroup(result);
    }
}