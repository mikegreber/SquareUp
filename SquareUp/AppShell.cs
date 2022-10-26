using CommunityToolkit.Maui.Markup;
using SquareUp.Resources.Themes;
using SquareUp.View;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace SquareUp;

public class AppShell : Shell
{
    public AppShell()
    {
        InitializeRouting();

        //Style = (Style<Shell>)Application.Current.Resources[nameof(ThemeBase.ShellStyle)];
        
        // BackgroundColor = Colors.Blue;
        // SetForegroundColor(this, Colors.Green);
        // SetTitleView(this, new CollectionView
        //     {
        //         HeightRequest = 40,
        //         IsGrouped = true
        //     }
        //     .Bind(ItemsView.ItemsSourceProperty, nameof(FlyoutItems))
        //     .ItemTemplate(new DataTemplate(() => new Grid
        //     {
        //         WidthRequest = 1800,
        //         HeightRequest = 50,
        //         Padding = 10,
        //         ColumnDefinitions = Columns.Define(
        //             (Column.Back, GridLength.Star),
        //             (Column.Title, GridLength.Star),
        //             (Column.Action, GridLength.Star)
        //         ),
        //
        //         RowDefinitions = Rows.Define((Row.First, 56)),
        //
        //         Children =
        //         {
        //             new Label()
        //                 {
        //                     BackgroundColor = Colors.Red
        //                 }
        //                 .Bind("Title")
        //                 .Column(Column.Back),
        //
        //             new Label()
        //                 .Bind("Title")
        //                 .Column(Column.Title),
        //
        //             new Label()
        //                 .Bind("Title")
        //                 .Column(Column.Action)
        //         }
        //     })).BindingContext(this));
        // FlyoutWidth = 400;
        // FlyoutHeight = 400;
        // FlyoutBackgroundColor = Colors.LightGray;
        // FlyoutBehavior = FlyoutBehavior.Locked;
        // FlyoutContent = new CollectionView { IsGrouped = true }
        //     .Bind(ItemsView.ItemsSourceProperty, nameof(FlyoutItems))
        //     .ItemTemplate(new DataTemplate(() => new Grid
        //     {
        //         
        //         Padding = 10,
        //         ColumnDefinitions = Columns.Define(
        //             (Column.Back, GridLength.Star),
        //             (Column.Title, GridLength.Star),
        //             (Column.Action, GridLength.Star)
        //         ),
        //         
        //         RowDefinitions = Rows.Define((Row.First, 56)),
        //
        //         Children =
        //         {
        //             new Label()
        //                 .Bind("Title")
        //                 .Column(Column.Back),
        //
        //             new Label()
        //                 .Bind("Title")
        //                 .Column(Column.Title),
        //
        //             new Label()
        //                 .Bind("Title")
        //                 .Column(Column.Action)
        //         }
        //     })).BindingContext(this);
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

    private enum Row
    {
        First
    }

    private enum Column
    {
        Back,
        Title,
        Action
    }
}