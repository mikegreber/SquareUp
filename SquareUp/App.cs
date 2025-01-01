using SquareUp.View;
using SquareUp.Resources.Themes;

namespace SquareUp;

public class App : Application
{
    private readonly LoginPage _loginPage;
    
    public App(LoginPage loginPage)
    {
        _loginPage = loginPage;
        SetAppTheme(RequestedTheme);

        RequestedThemeChanged += HandleRequestedThemeChanged;
    }
    
    protected override Window CreateWindow(IActivationState? activationState) => new(new AppShell(_loginPage));

    private void HandleRequestedThemeChanged(object sender, AppThemeChangedEventArgs e) =>
        SetAppTheme(e.RequestedTheme);


    private void SetAppTheme(in AppTheme appTheme) => Resources = appTheme switch
    {
        AppTheme.Dark => new DarkTheme(),
        _ => new DarkTheme()
    };
}
