using CommunityToolkit.Maui.Markup;
using SquareUp.Controls;
using SquareUp.Library;
using SquareUp.Model;
using SquareUp.Resources.Statics;
using SquareUp.Resources.Themes;
using SquareUp.Shared.Models;
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
            animate: false,
            parameters: TransactionViewModel.Params(transaction, mode, title));
    }

    private enum Row { First }
    private enum Column { Left, Middle, Right }

    public TransactionPage(TransactionViewModel viewModel) : base(viewModel)
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
                                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor))
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
                        .Bind<Grid, string, Brush>(BackgroundProperty, "Session.Group.Color", convert: Converters.ConvertBackground),

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

                    new CollectionView { SelectionMode = SelectionMode.Single, }
                        .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.Participant")
                        .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Participants")
                        .FillHorizontal()
                        .ItemTemplate(new DataTemplate(() => new Label()
                            .Bind("Name")
                            .Margin(16,0)
                            .Padding(0)
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
                            ItemsUpdatingScrollMode = ItemsUpdatingScrollMode.KeepItemsInView,
                            ItemsLayout = new GridItemsLayout(4, ItemsLayoutOrientation.Vertical),
                            SelectionMode = SelectionMode.Single,
                            ItemsSource = TransactionCategories.All,
                        }
                        .Margin(16,0)
                        .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.Category")
                        .FillHorizontal()
                        .ItemTemplate(new CategoryGridItemTemplate()),

                    new Label()
                        .Text("Paid to")
                        .Bind<Label, string, string>("Transaction.SecondaryParticipant.Name", convert: name => name != null ? $"Paid to {name}" : "Paid to")
                        .Bind<Label, string, bool>(IsVisibleProperty, "Transaction.Category", convert: type => type == Transfer)
                        .DynamicResource(StyleProperty, nameof(ThemeBase.CategoryHeaderStyle)),

                    new CollectionView { SelectionMode = SelectionMode.Single, }
                        .Bind(SelectableItemsView.SelectedItemProperty, "Transaction.SecondaryParticipant")
                        .Bind(ItemsView.ItemsSourceProperty, "Session.Group.Participants")
                        .Bind<CollectionView, string, bool>(IsVisibleProperty, "Transaction.Category", convert: type => type == Transfer)
                        .FillHorizontal()
                        .ItemTemplate(new DataTemplate(() => new Label()
                            .Bind("Name")
                            .Margin(16,0)
                            .Padding(0)
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
                            .Margin(16,0)
                            .Padding(0)
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