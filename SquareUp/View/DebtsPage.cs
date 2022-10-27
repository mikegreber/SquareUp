using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Markup;
using SquareUp.Controls;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.Shared.Types;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class DebtsPage : BaseContentPage<DebtsViewModel>
{
    public static async Task OpenAsync(ObservableGroup group)
    {
        await Shell.Current.GoToAsync(state:
            nameof(DebtsPage),
            animate: true,
            parameters: DebtsViewModel.Params(group));
    }
    public DebtsPage(DebtsViewModel viewModel) : base(in viewModel)
    {
        Title = "Debts";
        BackButton = "< Group";

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new CollectionView()
                        .Margins(16, 0, 16, 16)
                        .Bind(ItemsView.ItemsSourceProperty, "Debts")
                        .Bind<CollectionView, FullyObservableCollection<Debt>, double>(CollectionView.HeightRequestProperty, "Debts", convert: d => d != null ? DebtListItem.ItemHeight * d.Count : 0.0)
                        .ItemTemplate(new DebtListItemTemplate()),

                    new Label()
                        .Text("Square Up")
                        .Padding(12, 6)
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),

                    new CollectionView()
                        .Margins(16, 0, 16, 16)
                        .Bind<CollectionView, FullyObservableCollection<Settlement>, double>(CollectionView.HeightRequestProperty, "Settlements", convert: d => d != null ? SettlementListItem.ItemHeight * d.Count : 200)
                        .Bind(ItemsView.ItemsSourceProperty, "Settlements")
                        .ItemTemplate(new SettlementListItemTemplate(BindingContext.SettleCommand))
                }
            }.BindingContext(BindingContext)
        };
    }
}