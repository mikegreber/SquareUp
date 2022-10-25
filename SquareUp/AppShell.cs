using SquareUp.View;

namespace SquareUp;

public class AppShell : Shell
{
    

    public AppShell()
    {
        InitializeRouting();
    }

    public static void InitializeRouting()
    {
        Routing.RegisterRoute(nameof(GroupsPage), typeof(GroupsPage));
        Routing.RegisterRoute(nameof(GroupDetailsPage), typeof(GroupDetailsPage));
        Routing.RegisterRoute(nameof(GroupPage), typeof(GroupPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(DebtsPage), typeof(DebtsPage));
        Routing.RegisterRoute(nameof(TransactionPage), typeof(TransactionPage));
    }
}