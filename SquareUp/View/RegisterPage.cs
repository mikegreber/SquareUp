using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class RegisterPage : BaseContentPage<RegisterViewModel>
{
    public RegisterPage(RegisterViewModel viewModel) : base(viewModel)
    {
        Title = "Register";
        BackButton = "< Login";

        Content = new StackLayout
        {
            MaximumWidthRequest = 600,
            Children =
            {
                new Label()
                    .Text("Name")
                    .Margins(bottom:4)
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry { Placeholder = "name..." }
                    .Margins(bottom:12)
                    .Bind(Entry.TextProperty, "RegisterRequest.Name"),

                new Label()
                    .Text("Email")
                    .Margins(bottom:4)
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry { Placeholder = "example@mail.com..." }
                    .Margins(bottom:12)
                    .Bind(Entry.TextProperty, "RegisterRequest.Email"),

                new Label()
                    .Text("Password")
                    .Margins(bottom:4)
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry { IsPassword = true, Placeholder = "****" }
                    .Margins(bottom:12)
                    .Bind(Entry.TextProperty, "RegisterRequest.Password"),

                new Label()
                    .Text("Confirm password")
                    .Margins(bottom:4)
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry{ IsPassword = true, Placeholder = "****" }
                    .Margins(bottom:24)
                    .Bind(Entry.TextProperty, "RegisterRequest.ConfirmPassword"),

                new Button()
                    .Text("Register")
                    .Margins(bottom: 30)
                    .BindCommand(nameof(BindingContext.RegisterCommand))
                    .DynamicResource(StyleProperty, nameof(ThemeBase.ButtonCreateStyle))
            }
        }.CenterVertical().Margin(30);
    }

    public static async Task OpenAsync()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}