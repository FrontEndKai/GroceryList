using SmartGroceryList.ViewModels;

namespace SmartGroceryList.Views;

public partial class GroceryListDetailPage : ContentPage
{
    public GroceryListDetailPage(GroceryListDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
