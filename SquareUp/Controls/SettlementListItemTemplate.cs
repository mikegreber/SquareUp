using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using SquareUp.Model;
using SquareUp.Resources.Themes;
using SquareUp.View;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace SquareUp.Controls;

public class SettlementListItemTemplate : DataTemplate
{
    public SettlementListItemTemplate(IRelayCommand<Settlement> onTap = null) :
        base(() => new SettlementListItem().Bind(SettlementListItem.SettlementProperty)
            .Bind(SettlementListItem.CommandProperty, source: onTap))
    { }
}

public class SettlementListItem : ContentView
{
    public static readonly BindableProperty SettlementProperty = BindableProperty.Create(
        nameof(Settlement),
        typeof(Settlement),
        typeof(SettlementListItem),
        new Settlement
            { From = new ObservableParticipant { Name = "From" }, To = new ObservableParticipant { Name = "To" } }
    );

    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(IRelayCommand<Settlement>),
        typeof(SettlementListItem),
        defaultBindingMode: BindingMode.OneWay
    );

    public SettlementListItem()
    {
        Content = new VerticalStackLayout
        {
            Children =
            {
                new Grid
                {
                    ColumnDefinitions = Columns.Define((Column.First, Stars(3)), (Column.Second, Star)),
                    RowDefinitions = Rows.Define((Row.First, 50)),

                    Children =
                    {
                        new Label()
                            .Bind<Label, Settlement, string>(convert: s => s != null ? $"{s.From.Name} pays ${s.Amount:0.00} to {s.To.Name}" : "")
                            .CenterVertical()
                            .Column(Column.First)
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor)),

                        new Button()
                            .Text("Settle")
                            .FillHorizontal()
                            .CenterVertical()
                            .Column(Column.Second)
                            .BindCommand(nameof(Command), parameterPath: nameof(Settlement))
                            .DynamicResource(StyleProperty, nameof(ThemeBase.AltButtonStyle))
                            .BindingContext(this)
                    }
                },

                new BoxView()
                    .Height(1)
                    .FillHorizontal()
                    .DynamicResource(BoxView.ColorProperty, nameof(ThemeBase.DividerColor))
            }
        };
    }

    public Settlement Settlement
    {
        get => (Settlement)GetValue(SettlementProperty);
        set => SetValue(SettlementProperty, value);
    }

    public IRelayCommand<Settlement> Command
    {
        get => (IRelayCommand<Settlement>)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    private enum Column { First, Second }
    private enum Row { First }
}