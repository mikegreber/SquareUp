using CommunityToolkit.Maui.Markup;
using SquareUp.Controls;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class GroupsPage : BaseContentPage<GroupsViewModel>
{
    public static async Task OpenAsync()
    {
        await Shell.Current.GoToAsync(
            state: nameof(GroupsPage),
            animate: true
            // no parameters
        );
    }
    public GroupsPage(GroupsViewModel viewModel) : base(in viewModel)
    {
        Title = "Groups";
        BackButton = "< Sign Out";
        
        Content = new ScrollView
        {
            Content = new CollectionView
            {
                Margin = 0,
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
                .Margin(0)
                .Font(bold: true)
                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor))
                .Bind<Label, DateTime, string>(Label.TextProperty, "Key", convert: d => d.ToLongDateString().ToUpper()))
            }
            .Margin(new Thickness(0, 0, 0, 72))
            .Bind(ItemsView.ItemsSourceProperty, "Session.Groups")
            .ItemTemplate(new GroupCardTemplate(BindingContext.GroupTapCommand)),
        }
        .Padding(12, 0);
    }
}