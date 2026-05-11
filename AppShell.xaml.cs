namespace GroceryMate;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		RegisterRoutes();
		StartOnLogin();
	}

	private void RegisterRoutes()
	{
		Routing.RegisterRoute("add-product", typeof(Views.AddProductPage));
		Routing.RegisterRoute("product-detail", typeof(Views.ProductDetailPage));
		Routing.RegisterRoute("price-compare", typeof(Views.PriceComparePage));
	}

	private void StartOnLogin()
	{
		Dispatcher.Dispatch(async () => await GoToAsync("//login"));
	}
}
