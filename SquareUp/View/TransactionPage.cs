using CommunityToolkit.Maui.Markup;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Statics;
using SquareUp.Resources.Themes;
using SquareUp.Shared.Models;
using SquareUp.Shared.Types;
using SquareUp.ViewModel;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using static SquareUp.Resources.Statics.TransactionCategories;

namespace SquareUp.View;

public class TransactionPage : BaseContentPage<TransactionViewModel>
{
    public static async Task OpenAsync(ObservableTransaction transaction, PageMode mode, string title)
    {
        await Shell.Current.GoToAsync(state: 
            nameof(TransactionPage), 
            animate: true,
            parameters: TransactionViewModel.Params(transaction, mode, title));
    }

    public TransactionPage(TransactionViewModel viewModel) : base(viewModel)
    {
        BackButton = "< cancel";
        this.Bind(TitleProperty, nameof(BindingContext.Title));

        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Label()
                        .Text("Description")
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new Entry { Keyboard = Keyboard.Text }
                        .Placeholder("Transaction description...")
                        .Margin(16, 6)
                        .Bind(Entry.TextProperty, "Transaction.Name"),

                    new Label()
                        .Text("Amount")
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new Entry { Keyboard = Keyboard.Numeric }
                        .Margin(16, 6)
                        .Bind(Entry.TextProperty, "Transaction.Amount"),

                    new Label()
                        .Text("Paid by")
                        .Bind<Label, string, string>("Transaction.Participant.Name", convert: name => name != null ? $"Paid by {name}" : "Paid by")
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new CollectionView { SelectionMode = SelectionMode.Single }
                            .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.Participant")
                            .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Participants")
                            .Bind<CollectionView, FullyObservableCollection<ObservableParticipant>, int>(CollectionView.HeightRequestProperty, "Session.Group.Participants", convert: p => p != null ? p.Count * 40 : 100 )
                            .FillHorizontal()
                            .ItemTemplate(new DataTemplate(() => new Label()
                                .Bind("Name")
                                .Height(40)
                                .TextCenterVertical()
                                .Padding(20,0)
                                .Start()
                                .FillHorizontal()
                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
                            )),


                    new Label()
                        .Text("Category")
                        .Bind<Label, string, string>("Transaction.Category",  convert: category => category != null ? $"Category - {category.Split('*').LastOrDefault()}" : "Category")
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new CollectionView
                        {
                            ItemsLayout = new GridItemsLayout(4, ItemsLayoutOrientation.Vertical),
                            SelectionMode = SelectionMode.Single,
                            ItemsSource = TransactionCategories.All,
                        }
                        .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.Category")
                        .Bind<CollectionView, IReadOnlyCollection<string>, double>(CollectionView.HeightRequestProperty, source: TransactionCategories.All, convert: s => s != null ? (Math.Ceiling(s.Count/4.0) * CategoryGridItem.ItemHeight) : 100 )
                        .Margin(16,0)
                        .FillHorizontal()
                        .ItemTemplate(new CategoryGridItemTemplate()),

                    new Label()
                        .Text("Paid to")
                        .Bind<Label, string, string>("Transaction.SecondaryParticipant.Name", convert: name => name != null ? $"Paid to {name}" : "Paid to")
                        .Bind<Label, string, bool>(IsVisibleProperty, "Transaction.Category", convert: type => type == Transfer)
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new CollectionView { SelectionMode = SelectionMode.Single }
                            .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.SecondaryParticipant")
                            .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Participants")
                            .Bind<CollectionView, string, bool>(IsVisibleProperty, "Transaction.Category", convert: type => type == Transfer)
                            .Bind<CollectionView, FullyObservableCollection<ObservableParticipant>, int>(CollectionView.HeightRequestProperty, "Session.Group.Participants", convert: p => p != null ? p.Count * 40 : 100 )
                            .FillHorizontal()
                            .ItemTemplate(new DataTemplate(() => new Label()
                                .Bind("Name")
                                .Height(40)
                                .TextCenterVertical()
                                .Padding(20,0)
                                .Start()
                                .FillHorizontal()
                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
                            )),

                    new Label()
                        .Text("Split Type")
                        .Bind<Label, string, bool>(IsVisibleProperty, "Transaction.Category", convert: type => type != Income && type != Transfer)
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new CollectionView
                        {
                            SelectionMode = SelectionMode.Single,
                            ItemsSource = Enum.GetValues<SplitType>()[new Range(0,2)],
                        }
                        .Height(80)
                        .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.Type")
                        .Bind<CollectionView, string, bool>(IsVisibleProperty, "Transaction.Category", convert: type => type != Income && type != Transfer)
                        .FillHorizontal()
                        .ItemTemplate(new DataTemplate(() => new Label()
                            .Bind<Label, SplitType, string>(convert: type => type switch
                            {
                                SplitType.EvenlySplit => "Evenly Split",
                                SplitType.IncomeProportional => "Income Proportional",
                                _ => string.Empty
                            })
                            .Height(40)
                            .TextCenterVertical()
                            .Padding(16,0)
                            .Start()
                            .FillHorizontal()
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
                        )),

                    new Button()
                        .Text("Add Transaction")
                        .Margin(new Thickness(15, 30, 15, 15))
                        .Bind<Button, PageMode, bool>(IsVisibleProperty, nameof(PageMode), convert: mode => mode == PageMode.Create)
                        .BindCommand(nameof(BindingContext.CreateCommand), parameterPath: nameof(BindingContext.Transaction))
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.Yellow100Accent))
                        .DynamicResource(Button.TextColorProperty, nameof(ThemeBase.White)),

                    new Button()
                        .Text("Update Transaction")
                        .Margin(new Thickness(15, 30, 15, 15))
                        .Bind<Button, PageMode, bool>(IsVisibleProperty, nameof(PageMode), convert: mode => mode == PageMode.Edit)
                        .BindCommand(nameof(BindingContext.UpdateCommand), parameterPath: nameof(BindingContext.Transaction))
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.Yellow100Accent))
                        .DynamicResource(Button.TextColorProperty, nameof(ThemeBase.White)),

                    new Button()
                        .Text("Delete Transaction")
                        .Margin(new Thickness(15,0,15,30))
                        .Bind<Button, PageMode, bool>(IsVisibleProperty, nameof(PageMode), convert: mode => mode == PageMode.Edit)
                        .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.Cyan100Accent))
                        .DynamicResource(Button.TextColorProperty, nameof(ThemeBase.White))
                        .BindCommand(nameof(BindingContext.DeleteCommand), parameterPath: nameof(BindingContext.Transaction)),
                },

                
            }.BindingContext(BindingContext)
        };
    }
}