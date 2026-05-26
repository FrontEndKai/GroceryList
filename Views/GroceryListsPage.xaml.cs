using SmartGroceryList.ViewModels;

namespace SmartGroceryList.Views;

public partial class GroceryListsPage : ContentPage
{
    public GroceryListsPage(GroceryListsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GroceryListsViewModel vm)
            await vm.ReloadAsync();
    }
}
