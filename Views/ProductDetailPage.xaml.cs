using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

[QueryProperty(nameof(ProductId), "id")]
public partial class ProductDetailPage : ContentPage
{
    private readonly ProductDetailViewModel _viewModel;

    public ProductDetailPage()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<ProductDetailViewModel>();
        BindingContext = _viewModel;
    }

    public string ProductId
    {
        set => _viewModel.LoadProduct(value);
    }
}
