using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using SquareUp.Services.Auth;
using SquareUp.Services.Transactions;
using SquareUp.Services.Groups;
using SquareUp.Services.Navigation;
using SquareUp.Services.Users;
using SquareUp.ViewModel;
using SquareUp.View;
using SquareUp.Services.Session;
using SessionData = SquareUp.Services.Session.SessionData;

namespace SquareUp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMarkup()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews();


        return builder.Build();
	}

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton(DeviceInfo.Current);

#if RELEASE
        mauiAppBuilder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri("https://squareupserver20221005155638.azurewebsites.net") });
#elif WINDOWS
        mauiAppBuilder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });
#else
        mauiAppBuilder.Services.AddSingleton(_ => new HttpClient { BaseAddress = new Uri("http://192.168.1.76:5000/") });
#endif
        mauiAppBuilder.Services.AddSingleton<IUserService, UserService>();
        mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();
        mauiAppBuilder.Services.AddSingleton<IGroupService, GroupService>();
        mauiAppBuilder.Services.AddSingleton<ISessionData, SessionData>();
        mauiAppBuilder.Services.AddSingleton<ITransactionService, TransactionService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<TransactionViewModel>();
        mauiAppBuilder.Services.AddTransient<GroupsViewModel>();
        mauiAppBuilder.Services.AddSingleton<GroupViewModel>();
        mauiAppBuilder.Services.AddTransient<GroupDetailsViewModel>();
        mauiAppBuilder.Services.AddTransient<DebtsViewModel>();
        mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        mauiAppBuilder.Services.AddTransient<RegisterViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<TransactionPage>();
        mauiAppBuilder.Services.AddTransient<GroupsPage>();
        mauiAppBuilder.Services.AddTransient<GroupPage>();
        mauiAppBuilder.Services.AddTransient<GroupDetailsPage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        mauiAppBuilder.Services.AddSingleton<RegisterPage>();
        mauiAppBuilder.Services.AddTransient<DebtsPage>();

        return mauiAppBuilder;
    }
}
