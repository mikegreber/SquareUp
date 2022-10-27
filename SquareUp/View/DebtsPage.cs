using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.Shared.Types;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class DebtsPage : BaseContentPage<DebtsViewModel>
{
    public DebtsPage(DebtsViewModel viewModel) : base(in viewModel)
    {
        Title = "Debts";
        BackButton = "< Group";

        Content = new ScrollView
        {
            MaximumWidthRequest = 400,
            Content = new DebtsView(BindingContext).BindingContext(BindingContext),

            // Content = new VerticalStackLayout
            // {
            //     Children =
            //     {
            //         new CollectionView()
            //             .Margins(16, 0, 16, 16)
            //             .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Debts")
            //             .Bind<CollectionView, FullyObservableCollection<Debt>, double>(HeightRequestProperty, "Session.Group.Debts",
            //                 convert: d => d != null ? DebtListItem.ItemHeight * d.Count : 0.0)
            //             .ItemTemplate(new DebtListItemTemplate()),
            //
            //         new Label()
            //             .Text("Square Up")
            //             .Padding(12, 6)
            //             .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
            //             .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),
            //
            //         new CollectionView()
            //             .Margins(16, 0, 16, 16)
            //             .Bind<CollectionView, FullyObservableCollection<Settlement>, double>(HeightRequestProperty,
            //                 "Session.Group.Settlements", convert: d => d != null ? SettlementListItem.ItemHeight * d.Count : 200)
            //             .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Settlements")
            //             .ItemTemplate(new SettlementListItemTemplate(BindingContext.SettleCommand))
            //     }
            //}//.BindingContext(BindingContext)
        };
    }

    public static async Task OpenAsync(ObservableGroup group)
    {
        await Shell.Current.GoToAsync(nameof(DebtsPage),
            true
            // no params
            );
    }
}

public class DebtsView : BaseContentView<DebtsViewModel>
{
    public DebtsView(DebtsViewModel viewModel) : base(in viewModel)
    {
        Content = new VerticalStackLayout
        {
            Children =
            {
                new CollectionView()
                    .Margins(32, 0, 32, 16)
                    .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Debts")
                    .Bind<CollectionView, FullyObservableCollection<Debt>, double>(HeightRequestProperty, "Session.Group.Debts",
                        convert: d => d != null ? DebtListItem.ItemHeight * d.Count : 0.0)
                    .ItemTemplate(new DebtListItemTemplate()),

#if WINDOWS || MACCATALYST
                new Border
                    {
                        StrokeShape = new RoundRectangle { CornerRadius = 10 },
                        BackgroundColor = Colors.Pink,
                        Content = new Label().Text("Square Up").TextCenterHorizontal().TextCenterVertical().Font(size:20)
                    }
                    .Bind<Border, FullyObservableCollection<Settlement>, bool>(IsVisibleProperty, "Session.Group.Settlements", convert: s => s != null && s.Count > 0)
                    .Bind<Border, string, LinearGradientBrush>(BackgroundProperty, "Session.Group.Color",
                        convert: Converters.ConvertBackground)
                    .Margins(12,0,12,12),
#else 
                new Label()
                     .Text("Square Up")
                     .Padding(12, 6)
                     .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                     .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),
#endif


                new CollectionView()
                    .Margins(32, 0, 32, 16)
                    .Bind<CollectionView, FullyObservableCollection<Settlement>, double>(HeightRequestProperty,
                        "Session.Group.Settlements", convert: d => d != null ? SettlementListItem.ItemHeight * d.Count : 200)
                    .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Settlements")
                    .ItemTemplate(new SettlementListItemTemplate(BindingContext.SettleCommand))
            }
        }.BindingContext(BindingContext);
    }

    // public static async Task OpenAsync(ObservableGroup group)
    // {
    //     await Shell.Current.GoToAsync(nameof(DebtsView),
    //         true
    //         //no params
    //         );
    // }
}