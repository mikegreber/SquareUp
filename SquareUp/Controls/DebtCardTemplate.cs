using CommunityToolkit.Maui.Markup;
using SquareUp.Model;
using SquareUp.Shared.Types;

namespace SquareUp.Controls;

public class DebtCardTemplate : DataTemplate
{
    public DebtCardTemplate() : base(() => new DebtCard().Bind(DebtCard.DebtsProperty))
    {}


    public class DebtCard : ContentView
    {
        public static readonly BindableProperty DebtsProperty = BindableProperty.Create(
            propertyName: nameof(Debts),
            returnType: typeof(FullyObservableCollection<Debt>),
            declaringType: typeof(DebtCard),
            defaultBindingMode: BindingMode.OneWay
        );

        public FullyObservableCollection<Debt> Debts
        {
            get => (FullyObservableCollection<Debt>) GetValue(DebtsProperty);
            set => SetValue(DebtsProperty, value);
        }
        public DebtCard()
        {
            Content = new Frame()
            {
                BindingContext = this,
                Content = new CollectionView()
                    .Bind(ItemsView.ItemsSourceProperty, "Debts")
                    .ItemTemplate(new DataTemplate(() =>
                    {
                        var item = new HorizontalStackLayout()
                        {
                            Children =
                            {
                                new Label().Bind(Label.TextProperty, "User.Name"),
                                new Label().Bind(Label.TextProperty, "Proportion"),
                                new Label().Bind(Label.TextProperty, "Total"),
                            }
                        };

                        return item;
                    }))
            };
        }
    }

}