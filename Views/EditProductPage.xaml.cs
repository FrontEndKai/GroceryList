using SmartGroceryList.ViewModels;

namespace SmartGroceryList.Views;

public partial class EditProductPage : ContentPage
{
    public EditProductPage(EditProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
