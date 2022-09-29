
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using SquareUp.Services.Auth;
using SquareUp.Services.Groups;
using SquareUp.Services.Navigation;
using SquareUp.Services.Users;
using SquareUp.ViewModel;
using SquareUp.View;

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
        // mauiAppBuilder.Services.AddSingleton<App>();
        // mauiAppBuilder.Services.AddSingleton<AppShell>();
        mauiAppBuilder.Services.AddSingleton(DeviceInfo.Current);
        mauiAppBuilder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7199/") });
        mauiAppBuilder.Services.AddSingleton<IUserService, UserService>();
        mauiAppBuilder.Services.AddSingleton<IAuthService, AuthService>();
        mauiAppBuilder.Services.AddSingleton<IGroupService, GroupService>();
        mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
        
        // mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        // mauiAppBuilder.Services.AddSingleton<INavigationService, MauiNavigationService>();
        // mauiAppBuilder.Services.AddSingleton<IDialogService, DialogService>();
        // mauiAppBuilder.Services.AddSingleton<IOpenUrlService, OpenUrlService>();
        // mauiAppBuilder.Services.AddSingleton<IRequestProvider, RequestProvider>();
        // mauiAppBuilder.Services.AddSingleton<IIdentityService, IdentityService>();
        // mauiAppBuilder.Services.AddSingleton<IFixUriService, FixUriService>();
        // mauiAppBuilder.Services.AddSingleton<ILocationService, LocationService>();
        //
        // mauiAppBuilder.Services.AddSingleton<ITheme, Theme>();
        //
        // mauiAppBuilder.Services.AddSingleton<IAppEnvironmentService, AppEnvironmentService>(
        //     serviceProvider =>
        //     {
        //         var requestProvider = serviceProvider.GetService<IRequestProvider>();
        //         var fixUriService = serviceProvider.GetService<IFixUriService>();
        //         var settingsService = serviceProvider.GetService<ISettingsService>();
        //
        //         var aes =
        //             new AppEnvironmentService(
        //                 new BasketMockService(), new BasketService(requestProvider, fixUriService),
        //                 new CampaignMockService(), new CampaignService(requestProvider, fixUriService),
        //                 new CatalogMockService(), new CatalogService(requestProvider, fixUriService),
        //         new OrderMockService(), new OrderService(requestProvider),
        //                 new UserMockService(), new UserService(requestProvider));
        //
        //         aes.UpdateDependencies(settingsService.UseMocks);
        //         return aes;
        //     });

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainViewModel>();
        mauiAppBuilder.Services.AddSingleton<GroupsViewModel>();
        mauiAppBuilder.Services.AddTransient<GroupViewModel>();
        mauiAppBuilder.Services.AddSingleton<LoginViewModel>();
        // mauiAppBuilder.Services.AddSingleton<AddExpenseViewModel>();
        // mauiAppBuilder.Services.AddSingleton<AddPersonViewModel>();
        // mauiAppBuilder.Services.AddSingleton<ExpenseViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPage>();
        mauiAppBuilder.Services.AddSingleton<GroupsPage>();
        mauiAppBuilder.Services.AddTransient<GroupPage>();
        mauiAppBuilder.Services.AddSingleton<LoginPage>();
        // mauiAppBuilder.Services.AddTransient<AddPersonPage>();
        // mauiAppBuilder.Services.AddTransient<EditPersonPage>();
        // mauiAppBuilder.Services.AddTransient<AddExpensePage>();
        // mauiAppBuilder.Services.AddTransient<EditExpensePage>();
        

        return mauiAppBuilder;
    }
}
