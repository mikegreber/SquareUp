using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using SquareUp.Model;
using SquareUp.Shared.Models;

namespace SquareUp.Controls;

public class ExpenseCardTemplate : DataTemplate
{
    public ExpenseCardTemplate(OnTapDelegate? onTap = null) : 
        base(()=>new ExpenseCard(onTap).Bind(ExpenseCard.ExpenseProperty)) { }

    public delegate Task OnTapDelegate(Expense expense);

    public class ExpenseCard : ContentView
    {
        public static readonly BindableProperty ExpenseProperty = BindableProperty.Create(
            nameof(Expense),
            typeof(Expense),
            typeof(ExpenseCard),
            new Expense { Name = "DefaultName", Amount = 20m }
        );
        public Expense Expense
        {
            get => (Expense)GetValue(ExpenseProperty);
            set => SetValue(ExpenseProperty, value);
        }

        public ExpenseCard(OnTapDelegate? onTap = null)
        {
            BindingContext = this;

            Content = new Frame
            {
                GestureRecognizers =
                {
                    new TapGestureRecognizer()
                    {
                        Command = new Command(() => onTap?.Invoke(Expense))
                    }
                },
                Padding = 10,
                Margin = 5,
                BindingContext = this,
                Content = new FlexLayout
                {
                    Margin = 0,
                    HeightRequest = 50,
                    Padding = 0,
                    Direction = FlexDirection.Row,
                    JustifyContent = FlexJustify.SpaceBetween,
                    AlignContent = FlexAlignContent.Center,
                    BindingContext = this,
                    Children =
                {
                    new HorizontalStackLayout()
                    {
                        Children =
                        {
                            new Image()
                                .Bind(Image.SourceProperty, "Expense.User.Image")
                                .Size(30, 30)
                                .CenterHorizontal().Margin(10),

                            new BoxView()
                            {
                                WidthRequest = 2,
                                VerticalOptions = LayoutOptions.Fill,
                                Color = Colors.Grey,
                            },

                            new VerticalStackLayout
                            {
                                Children =
                                {
                                    new Label().Font(bold: true).Bind(Label.TextProperty, "Expense.Name"),
                                    new Label().Bind(Label.TextProperty, "Expense.User.Name"),
                                }
                            }.Margin(10, 0),
                        }
                    },

                    new VerticalStackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                TextColor = Colors.Orange,
                                HorizontalTextAlignment = TextAlignment.End
                            }
                            .Font(bold: true)
                            .Bind<Label, Expense, string>(Label.TextProperty, nameof(Expense),
                                convert: v => $"${v.Amount:0.00}"),

                            new Label()
                            {
                                HorizontalTextAlignment = TextAlignment.End
                            }
                            .Bind<Label, Expense, string>(Label.TextProperty, nameof(Expense),
                                convert: e => e.Type.ToString())
                        }
                    },
                }
                }
            };
        }

        
    }
}