using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GroceryMate;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<AppShell>();
		builder.Services.AddSingleton<Services.AuthService>();
		builder.Services.AddSingleton<Services.ProductCatalogService>();
		builder.Services.AddSingleton<Services.GroceryListService>();

		builder.Services.AddTransient<ViewModels.LoginViewModel>();
		builder.Services.AddTransient<ViewModels.SignupViewModel>();
		builder.Services.AddTransient<ViewModels.ProductsViewModel>();
		builder.Services.AddTransient<ViewModels.ProductDetailViewModel>();
		builder.Services.AddTransient<ViewModels.AddProductViewModel>();
		builder.Services.AddTransient<ViewModels.GroceryListViewModel>();
		builder.Services.AddTransient<ViewModels.PriceCompareViewModel>();

		builder.Services.AddTransient<Views.LoginPage>();
		builder.Services.AddTransient<Views.SignupPage>();
		builder.Services.AddTransient<Views.ProductsPage>();
		builder.Services.AddTransient<Views.ProductDetailPage>();
		builder.Services.AddTransient<Views.AddProductPage>();
		builder.Services.AddTransient<Views.GroceryListPage>();
		builder.Services.AddTransient<Views.PriceComparePage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		var app = builder.Build();
		App.Services = app.Services;
		return app;
	}
}
