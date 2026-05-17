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
            await vm.ReloadProductsAsync();
        }
    }
}
