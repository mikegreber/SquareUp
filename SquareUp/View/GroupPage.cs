using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
namespace SquareUp.View;

public class GroupPage : BaseContentPage<GroupViewModel>
{
    public static async Task OpenAsync(ObservableGroupInfo group)
    {
        await Shell.Current.GoToAsync(state:
            nameof(GroupPage),
            animate: true,
            parameters: GroupViewModel.Params(group));
    }

    private enum Column
    {
        First,
        Second
    }

    private enum Row
    {
        First,
        Second,
        Third
    }
    public GroupPage(GroupViewModel viewModel) : base(viewModel)
    {
        BackButton = "< Groups";
        AppBarActionButtonIconSource = "settings_white.png";
        AppBarActionCommand = BindingContext.TapSettingsCommand;

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Spacing = 0,
                Padding = 12,
                BindingContext = BindingContext,
                Children =
                        {
                             new Border
                                {
                                    BackgroundColor = Colors.Transparent,
                                    StrokeShape = new RoundRectangle { CornerRadius = 10 },
                                    Content = new Grid()
                                    {
                                        ColumnDefinitions = Columns.Define((Column.First, Star)),

                                        RowDefinitions = Rows.Define((Row.First, Auto), (Row.Second, Auto), (Row.Third, 56)),

                                        Children =
                                        {
                                            new Label()
                                                .Row(Row.First)
                                                .Padding(0)
                                                .Bind(Label.TextProperty, "Session.Group.Name")
                                                .Font(bold:true, size:24)
                                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White)),

                                            new Label()
                                                .Row(Row.Second)
                                                .Text("Total Debt")
                                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White)),

                                            new Label { VerticalTextAlignment = TextAlignment.End, }
                                                .BindTapGesture(nameof(BindingContext.TapDebtsCommand))
                                                .Row(Row.Third)
                                                .Text("Debts >")
                                                .Font(size:20)
                                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White)),
                                        }
                                    }
                                    .Paddings(20,16,20,20)
                                    .Bind<Grid, string, LinearGradientBrush>(BackgroundProperty, "Session.Group.Color", convert: Converters.ConvertBackground)
                                    .BindingContext(BindingContext),
                                },

                             new CollectionView
                                {
                                    Margin = 0,
                                    IsGrouped = true,
                                    ItemSizingStrategy = ItemSizingStrategy.MeasureAllItems,
                                    ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
                                    {
                                        ItemSpacing = 5,
                                    },
                                    GroupHeaderTemplate = new DataTemplate(()=> new Label
                                        {
                                            HeightRequest = 30,

                                            HorizontalOptions = LayoutOptions.Fill,
                                            HorizontalTextAlignment = TextAlignment.Center,
                                            VerticalTextAlignment = TextAlignment.End
                                        }
                                        .Margin(0)
                                        .Font(bold:true)
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor))
                                        .Bind<Label, DateTime, string>(Label.TextProperty, "Key", convert: d => d.ToLongDateString().ToUpper())
                                    )
                                }
                                .Margin(new Thickness(0, 0, 0, 72))
                                .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Transactions")
                                .ItemTemplate(new TransactionCardTemplate(BindingContext.TapTransactionCommand)),
                        }
            }
        };
    }
}