using SmartGroceryList.Views;

namespace SmartGroceryList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
		Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));
		Routing.RegisterRoute(nameof(GroceryListDetailPage), typeof(GroceryListDetailPage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(SignupPage), typeof(SignupPage));
	}
}
