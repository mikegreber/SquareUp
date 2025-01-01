using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls.Shapes;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.Shared.Types;
using SquareUp.ViewModel;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using Animation = Microsoft.Maui.Controls.Animation;

namespace SquareUp.View;

public class GroupDetailsPage : BaseContentPage<GroupDetailsViewModel>
{
    public static async Task OpenAsync(ObservableGroup group, PageMode mode, string title)
    {
        await Shell.Current.GoToAsync(state:
            nameof(GroupDetailsPage),
            animate: true,
            parameters: GroupDetailsViewModel.Params(group, mode, title));
    }

    public GroupDetailsPage(GroupDetailsViewModel viewModel) : base(viewModel)
    {
        BackButton = "Groups";

        this.Bind(TitleProperty, nameof(BindingContext.Title));

        Content = new ScrollView
        {
            MaximumWidthRequest = 400,
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label()
                        .Text("Name")
                        .FillHorizontal()
                        .Padding(12, 6)
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),

                    new Entry()
                        .Placeholder("Group Name")
                        .Margin(16, 6)
                        .Bind(Entry.TextProperty, "Group.Name")
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.PageBackgroundColor))
                        .Behaviors(new TextValidationBehavior
                            {
                                Flags = ValidationFlags.ValidateOnValueChanged,
                                MinimumLength = 3,
                                MaximumLength = 20,
                            }.Bind(ValidationBehavior.IsValidProperty, nameof(BindingContext.IsValidState))),

                    new Label()
                        .Text("Participants")
                        .FillHorizontal()
                        .Padding(12, 6)
                        .Bind<Label, PageMode, bool>(IsEnabledProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .Bind<Label, PageMode, bool>(IsVisibleProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),

                    new CollectionView()
                        .Bind(ItemsView.ItemsSourceProperty, "Group.Participants")
                        .Bind<CollectionView, FullyObservableCollection<ObservableParticipant>, double>(CollectionView.HeightRequestProperty, "Group.Participants", convert: p => p != null ? ParticipantListItem.ItemHeight * p.Count : 0.0)
                        .Bind(ParticipantListItem.ParticipantProperty)
                        .Bind<CollectionView, PageMode, bool>(IsEnabledProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .Bind<CollectionView, PageMode, bool>(IsVisibleProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .Margin(0, 0)
                        .ItemTemplate(new ParticipantListItemTemplate(BindingContext)),

                    new Label()
                        .BindTapGesture(nameof(BindingContext.AddParticipantCommand))
                        .Text("+ Add participant")
                        .Height(30)
                        .Margin(new Thickness(28, 8, 28, 12))
                        .TextCenterVertical()
                        .FillHorizontal()
                        .Bind<Label, PageMode, bool>(IsEnabledProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .Bind<Label, PageMode, bool>(IsVisibleProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.Blue100Accent)),

                    new Label()
                        .Text("Color")
                        .FillHorizontal()
                        .Padding(12, 6)
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),

                    new ScrollView
                    {
                        Orientation = ScrollOrientation.Horizontal,
                        Content = new CollectionView
                            {
                                
                                //ItemsLayout = new GridItemsLayout(5, ItemsLayoutOrientation.Vertical),
                                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal) { ItemSpacing = 5 },
                                //ItemSizingStrategy = ItemSizingStrategy.MeasureFirstItem,
                                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                                SelectionMode = SelectionMode.Single
                            }
                            .Start()
                            .Height(50)
                            .Margin(8)
                            .Bind(ItemsView.ItemsSourceProperty, source: ThemeBase.GroupColors)
                            .Bind(SelectableItemsView.SelectedItemProperty, "Group.Color")
                            .ItemTemplate(new DataTemplate(() => new Ellipse()
                                .Size(50)
                                .CenterHorizontal()
                                .Bind<Ellipse, string, Brush>(Shape.FillProperty, convert: Converters.ConvertBackground))),
                    },

                    new Button()
                        .Text("Create Group")
                        .Margin(15, 15)
                        .BindCommand(nameof(BindingContext.CreateGroupCommand))
                        .Bind<Button, PageMode, bool>(IsVisibleProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Create)
                        .DynamicResource(StyleProperty, nameof(ThemeBase.ButtonCreateDisabledStyle))
                        .StyleToggle(nameof(BindingContext.IsValidState), Theme.ButtonCreateStyle, Theme.ButtonCreateDisabledStyle),

                    new Button()
                        .Text("Update Group")
                        .Margin(15, 15)
                        .BindCommand(nameof(BindingContext.UpdateGroupCommand))
                        .Bind<Button, PageMode, bool>(IsVisibleProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .DynamicResource(StyleProperty, nameof(ThemeBase.ButtonUpdateDisabledStyle))
                        .StyleToggle(nameof(BindingContext.IsValidState), Theme.ButtonUpdateStyle, Theme.ButtonUpdateDisabledStyle),

                    new Button()
                        .Text("Delete Group")
                        .Margin(new Thickness(15, 0, 15, 30))
                        .BindCommand(nameof(BindingContext.TapDeleteGroupCommand))
                        .Bind<Button, PageMode, bool>(IsVisibleProperty, nameof(BindingContext.Mode), convert: mode => mode == PageMode.Edit)
                        .DynamicResource(StyleProperty, nameof(ThemeBase.ButtonDeleteStyle)),

                },
            }.BindingContext(BindingContext)
        };
    }
}

public class LerpStyle : TriggerAction<VisualElement>
{
    public LerpStyle(Style from, Style to)
    {
        From = from;
        To = to;
    }

    public LerpStyle(object from, object to)
    {
        From = (Style)from;
        To = (Style)to;
    }

    public Style From { get; set; }
    public Style To { get; set; }

    protected override void Invoke(VisualElement sender)
    {
        sender.Animate(
            "LerpStyle",
            new Animation(d => { sender.Style = From.GenericLerp(To, d); }),
            length: 800,
            easing: Easing.CubicOut);
    }
}
