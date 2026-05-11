using System.Collections.ObjectModel;
using GroceryMate.Models;
using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class PriceCompareViewModel : BaseViewModel
{
    private readonly ProductCatalogService _catalogService;
    private Product? _selectedProduct;
    private ObservableCollection<PriceOption> _priceOptions = new();

    public PriceCompareViewModel(ProductCatalogService catalogService)
    {
        _catalogService = catalogService;
        Products = _catalogService.Products;
    }

    public ObservableCollection<Product> Products { get; }

    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            if (SetProperty(ref _selectedProduct, value))
            {
                LoadOptions(value);
            }
        }
    }

    public ObservableCollection<PriceOption> PriceOptions
    {
        get => _priceOptions;
        set => SetProperty(ref _priceOptions, value);
    }

    public void SetProductById(string productId)
    {
        var product = _catalogService.GetById(productId);
        if (product is not null)
        {
            SelectedProduct = product;
        }
    }

    private void LoadOptions(Product? product)
    {
        if (product is null)
        {
            PriceOptions = new ObservableCollection<PriceOption>();
            return;
        }

        PriceOptions = new ObservableCollection<PriceOption>(_catalogService.GetPriceOptions(product.Id));
    }
}
