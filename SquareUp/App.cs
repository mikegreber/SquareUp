using SquareUp.View;
using SquareUp.Resources.Themes;

namespace SquareUp;

public class App : Application
{
    

    public App(LoginPage loginPage)
    {
        SetAppTheme(RequestedTheme);

        RequestedThemeChanged += HandleRequestedThemeChanged;

        MainPage = new AppShell { Items = { loginPage } };
    }

    private void HandleRequestedThemeChanged(object sender, AppThemeChangedEventArgs e) =>
        SetAppTheme(e.RequestedTheme);


    private void SetAppTheme(in AppTheme appTheme) => Resources = appTheme switch
    {
        AppTheme.Dark => new DarkTheme(),
        _ => new DarkTheme()
    };
}
