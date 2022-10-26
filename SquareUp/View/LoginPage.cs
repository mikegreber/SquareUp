using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class LoginPage : BaseContentPage<LoginViewModel>
{
    public LoginPage(LoginViewModel viewModel) : base(viewModel)
    {
        Title = "Login";

        Content = new StackLayout
        {
            MaximumWidthRequest = 400,
            Children =
                    {
                        new Label { Text = "Email" }
                            .Text("Email")
                            .Margins(bottom:4)
                            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                        new Entry { Placeholder = "example@mail.com" }
                            .Bind(Entry.TextProperty, "LoginRequest.Email")
                            .Margins(bottom:12)
                            .DynamicResource(Entry.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                        
                        new Label { Text = "Password" }
                            .Margins(bottom:4)
                            .DynamicResource(Label.TextColorProperty,
                            nameof(ThemeBase.SecondaryTextColor)),
                        new Entry{ IsPassword = true, Placeholder = "****" }
                            .Margins(bottom:24)
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
        }.CenterVertical().Margin(30);
    }
}