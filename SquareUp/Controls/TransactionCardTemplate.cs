using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.Shared.Models;
using SquareUp.View;
using SquareUp.ViewModel;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
namespace SquareUp.Controls;

public partial class TransactionCardTemplate : DataTemplate
{
    public TransactionCardTemplate(IRelayCommand<ObservableTransaction>? onTap = null) : 
        base(()=>new TransactionCard(onTap).Bind(TransactionCard.TransactionProperty)) { }

    public partial class TransactionCard : ContentView
    {
        public static readonly BindableProperty TransactionProperty = BindableProperty.Create(
            nameof(Transaction),
            typeof(ObservableTransaction),
            typeof(TransactionCard),
            new ObservableTransaction { Name = "DefaultName", Amount = 20m }
        );
        public ObservableTransaction Transaction
        {
            get => (ObservableTransaction)GetValue(TransactionProperty);
            set => SetValue(TransactionProperty, value);
        }

        [RelayCommand]
        private async void OpenTransaction(ObservableTransaction transaction)
        {
            await TransactionPage.OpenAsync(transaction, PageMode.Edit, "Edit Transaction");
        }

        private enum Column { First, Second }
        private enum Row { First, Second }

        public TransactionCard(IRelayCommand<ObservableTransaction>? onTap = null)
        {
            BindingContext = this;

            Content = new Frame
                {
                    CornerRadius = 10,
                    GestureRecognizers = {
                        new TapGestureRecognizer { Command = onTap ?? OpenTransactionCommand }
                            .Bind(TapGestureRecognizer.CommandParameterProperty, nameof(Transaction)) },
                    
                    Content = new Grid()
                    {
                        ColumnDefinitions = Columns.Define(
                            (Column.First, GridLength.Star),
                            (Column.Second, GridLength.Star)
                        ),

                        RowDefinitions = Rows.Define((Row.First, 56), (Row.Second, 20)),

                        Children =
                        {
                            new VerticalStackLayout()
                                {
                                    Children =
                                    {
                                        new Label()
                                            .Font(bold: true, size: 18)
                                            .Bind(Label.TextProperty, "Transaction.Name")
                                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor)),

                                        new Label()
                                            .Bind<Label, string, string>(Label.TextProperty, "Transaction.Category", convert: s => s != null ? string.Join(" ", s.Split('*').Reverse()) : string.Empty)
                                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardSecondaryTextColor))
                                            .Font(size:16)
                                    }
                                }
                                .Start()
                                .Margin(0)
                                .Padding(0)
                                .Column(Column.First).Row(Row.First),


                            new Label()
                                .Bind<Label, ObservableTransaction, string>(Label.TextProperty, "Transaction", convert: 
                                    transaction => transaction != null ? transaction.Type switch 
                                    {
                                        SplitType.EvenlySplit => $"paid by {transaction.Participant.Name}",
                                        SplitType.IncomeProportional => $"paid by {transaction.Participant.Name}",
                                        SplitType.Income => $"{transaction.Participant.Name}",
                                        SplitType.Payment => $"{transaction.Participant.Name} paid {transaction.SecondaryParticipant.Name}",
                                        _ => throw new ArgumentOutOfRangeException()
                                    } : string.Empty)
                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor))
                                .Column(Column.First)
                                .Row(Row.Second),

                            new Label()
                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardPrimaryTextColor))
                                .Font(bold: true)
                                .Bind<Label, ObservableTransaction, string>(Label.TextProperty, nameof(Transaction), convert: v => $"${v?.Amount:0.00}")
                                .End()
                                .Column(Column.Second)
                                .Row(Row.First),

                            new Label()
                                .Column(Column.Second)
                                .Row(Row.Second)
                                .End()
                                .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.TransactionCardSecondaryTextColor))
                                .Bind<Label, SplitType, string>(Label.TextProperty, "Transaction.Type",
                                    convert: type => type switch
                                    {
                                        SplitType.EvenlySplit => "Evenly split",
                                        SplitType.IncomeProportional => "Income proportional",
                                        _ => string.Empty
                                    } )
                        }
                    },
                }
                .Padding(12)
                .DynamicResource(BackgroundColorProperty, nameof(ThemeBase.TransactionCardBackgroundColor))
                .BindingContext(this);
            
        }

        
    }
}