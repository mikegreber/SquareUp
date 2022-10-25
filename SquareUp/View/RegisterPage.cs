using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using SquareUp.ViewModel;

namespace SquareUp.View;

public class RegisterPage : BaseContentPage<RegisterViewModel>
{
    public RegisterPage(RegisterViewModel viewModel) : base(viewModel)
    {
        BackView = new Label()
            .Text("< Login")
            .Font(size: 14)
            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor));

        TitleView = new Label()
            .Text("Register")
            .Font(size: 16)
            .CenterVertical()
            .CenterHorizontal()
            .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.PrimaryTextColor));

        Content = new StackLayout
        {
            Margin = 30,
            MaximumWidthRequest = 600,
            Children =
            {
                new Label()
                    .Text("Name")
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry { Placeholder = "name..." }
                    .Bind(Entry.TextProperty, "RegisterRequest.Name"),

                new Label()
                    .Text("Email")
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry { Placeholder = "example@mail.com..." }
                    .Bind(Entry.TextProperty, "RegisterRequest.Email"),

                new Label() 
                    .Text("Password")
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry { IsPassword = true, Placeholder = "****" }
                    .Bind(Entry.TextProperty, "RegisterRequest.Password"),

                new Label()
                    .Text("Confirm password")
                    .DynamicResource(Label.TextColorProperty, nameof(ThemeBase.SecondaryTextColor)),
                new Entry{ IsPassword = true, Placeholder = "****" }
                    .Bind(Entry.TextProperty, "RegisterRequest.ConfirmPassword"),

                new Button()
                    .Margins(top: 30, bottom: 30)
                    .Text("Register")
                    .BindCommand(nameof(BindingContext.RegisterCommand))
                    .DynamicResource(StyleProperty, nameof(ThemeBase.ButtonCreateStyle))
            }
        };
    }

    public static async Task OpenAsync()
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}