using GroceryMate.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace GroceryMate.Views;

[QueryProperty(nameof(ProductId), "id")]
public partial class PriceComparePage : ContentPage
{
    private readonly PriceCompareViewModel _viewModel;

    public PriceComparePage()
    {
        InitializeComponent();
        _viewModel = App.Services.GetRequiredService<PriceCompareViewModel>();
        BindingContext = _viewModel;
    }

    public string ProductId
    {
        set => _viewModel.SetProductById(value);
    }
}
