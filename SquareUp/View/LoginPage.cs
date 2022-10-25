using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class LoginPage : BaseContentPage<LoginViewModel>
{
    public LoginPage(LoginViewModel viewModel) : base(viewModel)
    {
        TitleView = new Label()
            .Text("Login")
            .Font(size:16)
            .CenterVertical()
            .CenterHorizontal()
            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor));

        Content = new StackLayout
        {
            Margin = 30,
            MaximumWidthRequest = 600,
            Children =
                    {
                        new Label { Text = "Email" }
                            .Text("Email")
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                        new Entry { Placeholder = "example@mail.com" }
                            .Bind(Entry.TextProperty, "LoginRequest.Email")
                            .DynamicResource(Entry.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                        
                        new Label { Text = "Password" }
                            .DynamicResource(Label.TextColorProperty,
                            nameof(ThemeBase.SecondaryTextColor)),
                        new Entry{ IsPassword = true, Placeholder = "****" }
                            .Bind(Entry.TextProperty, "LoginRequest.Password"),
                        
                        new Button()
                            .Text("Login")
                            .BindCommand(nameof(BindingContext.LoginCommand))
                            .DynamicResource(Button.StyleProperty, nameof(ThemeBase.ButtonCreateStyle)),

                        new Label()
                            .Text("Register")
                            .CenterHorizontal()
                            .Padding(12)
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor))
                            .BindTapGesture(nameof(BindingContext.RegisterCommand))
                    }
        };
    }
}