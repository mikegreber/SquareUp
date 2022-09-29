using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using SquareUp.Model;
using SquareUp.Shared.Models;

namespace SquareUp.Controls;

public class PersonListItem : ContentView
{
    public static readonly BindableProperty PersonProperty = BindableProperty.Create(
        nameof(Person),
        typeof(User),
        typeof(PersonListItem),
        new User { Name = "DefaultName" }
    );

    public User Person
    {
        get => (User)GetValue(PersonProperty);
        set => SetValue(PersonProperty, value);
    }

    public PersonListItem()
    {
        BindingContext = this;
        Content = new Frame
        {
            Padding = 10,
            Margin = 5,
            Content = new FlexLayout
            {
                BindingContext = this,
                Margin = 0,
                HeightRequest = 50,
                Padding = 0,
                Direction = FlexDirection.Row,
                JustifyContent = FlexJustify.SpaceBetween,
                AlignItems = FlexAlignItems.Center,
                Children =
                {
                    new HorizontalStackLayout
                    {
                        BindingContext = this,
                        Children =
                        {
                            new Image()
                                .Source(Person.Image)
                                .Size(30, 30)
                                .CenterHorizontal().Margin(10),

                            new BoxView
                            {
                                WidthRequest = 2,
                                VerticalOptions = LayoutOptions.Fill,
                                Color = Colors.Grey
                            },

                            new Label
                            {
                                VerticalTextAlignment = TextAlignment.Center
                            }
                            .Margin(10)
                            .Bind<Label, User, string>(Label.TextProperty, nameof(Person),
                                convert: e => e != null ? e.Name : "NULL"),

                            new Label
                            {
                                VerticalTextAlignment = TextAlignment.Center
                            }.Bind<Label, User, string>(Label.TextProperty,
                                nameof(Person), convert: GetTransactionString),
                            
                        }
                    },

                    new Label { VerticalTextAlignment = TextAlignment.Center }
                        .Bind<Label, User, string>(Label.TextProperty, nameof(Person),
                            convert: GetIncomeString)
                }
            }
        };
    }

    

    private string GetTransactionString(User? p)
    {
        if (p == null) return "NULL";

        return $"Spent ?";
        // return $"Spent {p.Expenses.Sum(e => e.Amount):0.00} on {p.Expenses.Count}";
    }

    private string GetIncomeString(User? p)
    {
        if (p == null) return "NULL";

        // return $"Income\n ${p.Income:0.00}";
        return $"Income\n ?";
    }
}