using Microsoft.Extensions.Logging;
using Microcharts.Maui;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Services;
using SmartGroceryList.ViewModels;
using SmartGroceryList.Views;

namespace SmartGroceryList;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMicrocharts()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
		builder.Services.AddSingleton<IProductService, ProductService>();
		builder.Services.AddSingleton<SmartGroceryList.Services.IThemeService, SmartGroceryList.Services.ThemeService>();

		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<ProductsViewModel>();
		builder.Services.AddSingleton<ProductsPage>();
		builder.Services.AddSingleton<SignupPage>();

		return builder.Build();
	}
}
