using System.Collections.ObjectModel;
using GroceryMate.Models;
using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class ProductDetailViewModel : BaseViewModel
{
    private readonly ProductCatalogService _catalogService;
    private readonly GroceryListService _groceryListService;
    private Product? _product;
    private ObservableCollection<PriceOption> _priceOptions = new();

    public ProductDetailViewModel(ProductCatalogService catalogService, GroceryListService groceryListService)
    {
        _catalogService = catalogService;
        _groceryListService = groceryListService;
        AddToListCommand = new Command(async () => await AddToListAsync());
        CompareCommand = new Command(async () => await CompareAsync());
    }

    public Product? Product
    {
        get => _product;
        set => SetProperty(ref _product, value);
    }

    public ObservableCollection<PriceOption> PriceOptions
    {
        get => _priceOptions;
        set => SetProperty(ref _priceOptions, value);
    }

    public Command AddToListCommand { get; }
    public Command CompareCommand { get; }

    public void LoadProduct(string productId)
    {
        var product = _catalogService.GetById(productId);
        if (product is null)
        {
            return;
        }

        Product = product;
        PriceOptions = new ObservableCollection<PriceOption>(_catalogService.GetPriceOptions(productId));
    }

    private async Task AddToListAsync()
    {
        if (Product is null)
        {
            return;
        }

        _groceryListService.AddProduct(Product);
        await UserFeedbackService.ShowSuccessAsync($"{Product.Name} added to your grocery list.");
    }

    private async Task CompareAsync()
    {
        if (Product is null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"price-compare?id={Product.Id}");
    }
}
