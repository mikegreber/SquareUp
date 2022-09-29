using SquareUp.Services.Navigation;
using SquareUp.View;
using SquareUp.ViewModel;

namespace SquareUp;

public class AppShell : Shell
{
    private readonly INavigationService _navigationService;

    public AppShell(LoginPage mainPage, INavigationService navigationService)
    {
        _navigationService = navigationService;

        AppShell.InitializeRouting();
        Items.Add(mainPage);
    }

    public static void InitializeRouting()
    {
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(GroupsPage), typeof(GroupsPage));
        Routing.RegisterRoute(nameof(GroupPage), typeof(GroupPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        // Routing.RegisterRoute(nameof(AddPersonPage), typeof(AddPersonPage));
        // Routing.RegisterRoute(nameof(AddExpensePage), typeof(AddExpensePage));
        // Routing.RegisterRoute(nameof(EditExpensePage), typeof(EditExpensePage));
        // Routing.RegisterRoute(nameof(EditPersonPage), typeof(EditPersonPage));
    }


    // static readonly IReadOnlyDictionary<Type, string> pageRouteMappingDictionary = new Dictionary<Type, string>(new[]
    // {
    //     // CreateRoutePageMapping<MainPage, MainViewModel>(),
    //     CreateRoutePageMapping<AddExpensePage, ExpenseViewModel>(),
    //     CreateRoutePageMapping<AddPersonPage, AddPersonViewModel>(),
    // });

    // public static void GoToPage(string route, Dictionary<string, object> parameters = null)
    // {
    //     if (parameters != null)
    //     {
    //         Current.GoToAsync("//"+route, parameters);
    //     }
    //     else Current.GoToAsync("//"+route);
    // }

    // public static string GetRoute<TPage, TViewModel>() where TPage : BaseContentPage<TViewModel>
    //     where TViewModel : BaseViewModel
    // {
    //     if (!pageRouteMappingDictionary.TryGetValue(typeof(TPage), out var route))
    //     {
    //         throw new KeyNotFoundException($"No map for ${typeof(TPage)} was found on navigation mappings. Please register your ViewModel in {nameof(AppShell)}.{nameof(pageRouteMappingDictionary)}");
    //     }
    //
    //     return route;
    // }
    //
    // static KeyValuePair<Type, string> CreateRoutePageMapping<TPage, TViewModel>() where TPage : BaseContentPage<TViewModel>
    //     where TViewModel : BaseViewModel
    // {
    //     var route = CreateRoute();
    //     Routing.RegisterRoute(route, typeof(TPage));
    //
    //     return new KeyValuePair<Type, string>(typeof(TPage), route);
    //
    //     static string CreateRoute()
    //     {
    //         if (typeof(TPage) == typeof(AddExpensePage))
    //         {
    //             return $"//{nameof(AddExpensePage)}";
    //         }
    //
    //         if (typeof(TPage) == typeof(AddPersonPage))
    //         {
    //             return $"//{nameof(AddPersonPage)}";
    //         }
    //
    //         throw new NotSupportedException($"{typeof(TPage)} Not Implemented in {nameof(pageRouteMappingDictionary)}");
    //     }
    // }
}