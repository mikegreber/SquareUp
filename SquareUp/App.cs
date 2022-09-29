using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareUp.View;
using SquareUp.Services.Navigation;

namespace SquareUp;

public class App : Application
{
    private readonly INavigationService _navigationService;

    public App(LoginPage mainPage, INavigationService navigationService)
    {
        _navigationService = navigationService;
        MainPage = new AppShell(mainPage, navigationService);
    }
}
