using SmartGroceryList.ViewModels;

namespace SmartGroceryList.Views;

public partial class AddProductPage : ContentPage
{
    public AddProductPage(AddProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is AddProductViewModel vm)
        {
            await vm.LoadCategoriesAsync();
        }
    }
}
