using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

public partial class ProductsPage : ContentPage
{
    public ProductsPage()
    {
        InitializeComponent();
        BindingContext = App.Services.GetRequiredService<ProductsViewModel>();
    }
}
