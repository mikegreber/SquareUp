using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls.Shapes;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Themes;
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
            animate: false,
            parameters: GroupDetailsViewModel.Params(group, mode, title));
    }

    private enum Row { First }
    private enum Column { Left, Middle, Right }

    public GroupDetailsPage(GroupDetailsViewModel viewModel) : base(viewModel)
    {
        ShowAppBar = false;

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Grid
                        {
                            ColumnDefinitions = Columns.Define(
                                (Column.Left, 55),
                                (Column.Middle, GridLength.Star),
                                (Column.Right, 55)
                            ),

                            RowDefinitions = Rows.Define((Row.First, 56)),

                            Children =
                            {
                                new Label()
                                    .Bind(Label.TextProperty, nameof(BindingContext.Title))
                                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White))
                                    .Font(size: 18, bold: true)
                                    .CenterHorizontal()
                                    .CenterVertical()
                                    .Column(Column.Middle),

                                new Image()
                                    .Source("close_white.png")
                                    .Size(20)
                                    .End()
                                    .BindTapGesture(nameof(BindingContext.BackCommand))
                                    .Column(Column.Right),
                            },
                        }
                        .Height(56)
                        .Padding(16,0)
                        .Margin(0)
                        .FillHorizontal()
                        .Bind<Grid, string, Brush>(BackgroundProperty, "Group.Color", convert: Converters.ConvertBackground),


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
                                ItemsLayout = new GridItemsLayout(5, ItemsLayoutOrientation.Vertical),
                                ItemSizingStrategy = ItemSizingStrategy.MeasureFirstItem,
                                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                                SelectionMode = SelectionMode.Single
                            }
                            .Start()
                            .Bind(ItemsView.ItemsSourceProperty, source: ThemeBase.GroupColors)
                            .Bind(SelectableItemsView.SelectedItemProperty, "Group.Color")
                            .ItemTemplate(new DataTemplate(() => new Ellipse()
                                .Size(40)
                                .Margin(5)
                                .CenterHorizontal()
                                .Bind<Ellipse, string, Brush>(Shape.FillProperty, convert: Converters.ConvertBackground)))
                    }.Paddings(8,4,8),

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
                BindingContext = BindingContext
            }
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
