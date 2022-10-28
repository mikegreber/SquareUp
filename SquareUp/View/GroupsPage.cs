using CommunityToolkit.Maui.Markup;
using SquareUp.Controls;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class GroupsPage : BaseContentPage<GroupsViewModel>
{
    public GroupsPage(GroupsViewModel viewModel) : base(in viewModel)
    {
        Title = "Groups";
        BackButton = "< Sign Out";
        Padding = 0;

        Content = new ScrollView
        {
            Content = new StackLayout()
            {
                Children =
                {
                    new StackLayout
                    {
                        MaximumWidthRequest = 400,
                        Children =
                        {
                            new CollectionView
                                {
                                    IsGrouped = true,
                                    ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                                    ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical) { ItemSpacing = 5 },
                                    GroupHeaderTemplate = new DataTemplate(() => new Label
                                        {
                                            HeightRequest = 30,
                                            HorizontalOptions = LayoutOptions.Fill,
                                            HorizontalTextAlignment = TextAlignment.Start,
                                            VerticalTextAlignment = TextAlignment.End
                                        }
                                        .Margin(12, 0)
                                        .Font(bold: true)
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor))
                                        .Bind<Label, DateTime, string>(Label.TextProperty, "Key",
                                            convert: d =>
                                            {
                                                var s = d.ToLongDateString().Split(' ');
                                                return (s[1] + " " + s[3]).ToUpper();
                                            }))
                                }
                                .FillHorizontal()
                                .Margins(bottom: 72)
                                .Bind(ItemsView.ItemsSourceProperty, "Session.Groups")
                                .ItemTemplate(new GroupCardTemplate(BindingContext.GroupTapCommand))
                        }
                    }.Padding(12, 0)
                }
            },
        };
    }

    public static async Task OpenAsync()
    {
        await Shell.Current.GoToAsync(
            nameof(GroupsPage),
            true
            // no parameters
        );
    }
}