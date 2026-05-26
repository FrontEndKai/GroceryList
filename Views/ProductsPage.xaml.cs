using SmartGroceryList.ViewModels;

namespace SmartGroceryList.Views;

public partial class ProductsPage : ContentPage
{
	public ProductsPage(ProductsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ProductsViewModel vm)
        {
	        MainThread.BeginInvokeOnMainThread(async () =>
	        {
		        await vm.ReloadProductsAsync();
	        });
        }
    }
}
