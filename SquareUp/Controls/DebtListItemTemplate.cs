using CommunityToolkit.Maui.Markup;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace SquareUp.Controls;

public class DebtListItemTemplate : DataTemplate
{
    public DebtListItemTemplate() :
        base(() => new DebtListItem().Bind(DebtListItem.DebtProperty))
    { }
}

public class DebtListItem : ContentView
{
    public static readonly BindableProperty DebtProperty = BindableProperty.Create(
        nameof(Debt),
        typeof(Debt),
        typeof(DebtListItem)
    );

    public static double ItemHeight = 50;

    public DebtListItem()
    {
        Content = new Grid
        {
            ColumnDefinitions = Columns.Define((Column.First, Star), (Column.Second, Star)),
            RowDefinitions = Rows.Define((Row.First, ItemHeight)),

            Children =
                    {
                        new Label()
                            .Bind("Participant.Name")
                            .CenterVertical()
                            .Row(Row.First)
                            .Column(Column.First)
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),

                        new Label()
                            .CenterVertical()
                            .End()
                            .Bind<Label, decimal, Color>(Label.TextColorProperty, "Amount", convert: balance =>
                                (Color)(balance switch
                                {
                                    < -0.1m => Application.Current.Resources[nameof(ThemeBase.PositiveTextColor)],
                                    > 0.1m => Application.Current.Resources[nameof(ThemeBase.NegativeTextColor)],
                                    _ => Application.Current.Resources[nameof(ThemeBase.Blue100Accent)]
                                }))
                            .Bind<Label, decimal, string>(Label.TextProperty, "Amount", convert: d => d switch
                            {
                                < -0.1m => $"owed ${-d:0.00}",
                                > 0.1m => $"owes ${d:0.00}",
                                _ => "square",
                            })
                            .Column(Column.Second),

                        new BoxView() { VerticalOptions = LayoutOptions.End }
                            .Height(1)
                            .Column(Column.First, Column.Second)
                            .FillHorizontal()
                            .DynamicResource(BoxView.ColorProperty, nameof(ThemeBase.DividerColor))
                    }
        };
    }

    public Debt Debt
    {
        get => (Debt)GetValue(DebtProperty);
        set => SetValue(DebtProperty, value);
    }

    private enum Column { First, Second }
    private enum Row { First }
}