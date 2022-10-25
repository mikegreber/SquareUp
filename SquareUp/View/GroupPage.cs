using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

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
    public GroupPage(GroupViewModel viewModel) : base(viewModel)
    {
        BackView = new Label()
            .Text("< Groups")
            .CenterHorizontal()
            .CenterVertical()
            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor));
        
        ActionView = new Image()
            .Source("settings_white.png")
            .Size(40)
            .BindTapGesture(nameof(BindingContext.TapSettingsCommand))
            .BindingContext(BindingContext);

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Spacing = 0,
                Padding = 12,
                BindingContext = BindingContext,
                Children =
                        {
                            new Frame
                            {
                                Padding = new Thickness(20, 16,20,20),
                                CornerRadius = 10,
                                BindingContext = BindingContext,
                                Content = new VerticalStackLayout
                                {
                                    Children =
                                    {
                                        new Label()
                                            .Padding(0)
                                            .Bind(Label.TextProperty, "Session.Group.Name")
                                            .Font(bold:true, size:24)
                                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White)),

                                        new Label()
                                            .Text("Total Debt")
                                            .Margin(new Thickness(0,4,0,40))
                                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White)),

                                        new FlexLayout
                                        {
                                            HeightRequest = 30,
                                            HorizontalOptions = LayoutOptions.Fill,
                                            AlignContent = FlexAlignContent.SpaceBetween,
                                            AlignItems = FlexAlignItems.Stretch,
                                            Direction = FlexDirection.Row,
                                            Children =
                                            {
                                                new Label
                                                    {
                                                        VerticalTextAlignment = TextAlignment.Center,
                                                        GestureRecognizers =
                                                        {
                                                            new TapGestureRecognizer()
                                                                .BindCommand(nameof(BindingContext.TapDebtsCommand))
                                                        }

                                                    }
                                                    .Text("Debts >")
                                                    .Font(size:20)
                                                    .Grow(0.75f)
                                                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.White)),

                                                // new Image()
                                                //     .Source("dotnet_bot.png")
                                                //     .Size(40)
                                                //     .BindTapGesture(nameof(BindingContext.TapSettingsCommand)),

                                            }
                                        }
                                    }
                                }
                            }.Bind<Frame, string, LinearGradientBrush>(BackgroundProperty, "Session.Group.Color", convert: Converters.ConvertBackground),

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
                                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.Gray50))
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