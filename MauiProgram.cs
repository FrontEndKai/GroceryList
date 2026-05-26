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

		// Services
		builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
		builder.Services.AddSingleton<IProductService, ProductService>();
		builder.Services.AddSingleton<IGroceryListService, GroceryListService>();
		builder.Services.AddSingleton<IFavoriteService, FavoriteService>();
		builder.Services.AddSingleton<IThemeService, ThemeService>();

		// ViewModels
		builder.Services.AddSingleton<LoginViewModel>();
		builder.Services.AddSingleton<ProductsViewModel>();
		builder.Services.AddSingleton<GroceryListsViewModel>();
		builder.Services.AddSingleton<FavoritesViewModel>();
		builder.Services.AddTransient<AddProductViewModel>();
		builder.Services.AddTransient<EditProductViewModel>();
		builder.Services.AddTransient<GroceryListDetailViewModel>();

		// Pages
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<SignupPage>();
		builder.Services.AddSingleton<ProductsPage>();
		builder.Services.AddSingleton<GroceryListsPage>();
		builder.Services.AddSingleton<FavoritesPage>();
		builder.Services.AddTransient<AddProductPage>();
		builder.Services.AddTransient<EditProductPage>();
		builder.Services.AddTransient<GroceryListDetailPage>();

		return builder.Build();
	}
}
