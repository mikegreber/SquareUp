using CommunityToolkit.Maui.Markup;
using SquareUp.Controls;
using SquareUp.Model;
using SquareUp.Resources.Themes;
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
                BindingContext = BindingContext,
                Children =
                {
                    new CollectionView()
                        .Margin(new Thickness(16, 0, 16, 16))
                        .Bind(ItemsView.ItemsSourceProperty, "Debts")
                        .ItemTemplate(new DebtListItemTemplate()),

                    new Label()
                        .Text("Square Up")
                        .Padding(12, 6)
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                        .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),

                    new CollectionView()
                        .Margins(16, 0, 16, 16)
                        .Bind(ItemsView.ItemsSourceProperty, "Settlements")
                        .ItemTemplate(new SettlementListItemTemplate(BindingContext.SettleCommand))
                }
            }
        };
    }
}