using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using SquareUp.View;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace SquareUp;

public class AppShell : Shell
{
    public AppShell(ShellItem root)
    {
        Items.Add(root);
        InitializeRouting();
    }

    private static void InitializeRouting()
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