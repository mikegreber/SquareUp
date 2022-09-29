using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Layouts;
using SquareUp.Model;

namespace SquareUp.Controls;

public class UserCardTemplate : DataTemplate
{
    public UserCardTemplate(OnTapDelegate? onTap = null) : 
        base(() => new UserCard(onTap).Bind(UserCard.UserProperty)) { }

    public delegate Task OnTapDelegate(User user);

    public class UserCard : ContentView
    {
        public static readonly BindableProperty UserProperty = BindableProperty.Create(
            nameof(User),
            typeof(User),
            typeof(UserCard),
            new User { Name = "DefaultName" }
        );

        public User User
        {
            get => (User)GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        public UserCard(OnTapDelegate? onTap = null)
        {
            BindingContext = this;
            Content = new Frame
            {
                GestureRecognizers =
                {
                    new TapGestureRecognizer
                    {
                        Command = new Command(() => onTap?.Invoke(User))
                    }
                },
                Padding = 10,
                Margin = 5,
                BindingContext = this,
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
                                    .Source(User.Image)
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
                                    .Bind<Label, User, string>(Label.TextProperty, nameof(User),
                                        convert: e => e != null ? e.Name : "NULL"),
                            }
                        },
                        new VerticalStackLayout(),

                    }
                }
            };
        }
    }
}