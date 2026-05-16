using System.Collections.ObjectModel;
using System.Collections.Specialized;
using GroceryMate.Models;
using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class ProductsViewModel : BaseViewModel
{
    private readonly ProductCatalogService _catalogService;
    private readonly GroceryListService _groceryListService;
    private string _searchText = string.Empty;

    public ProductsViewModel(ProductCatalogService catalogService, GroceryListService groceryListService)
    {
        _catalogService = catalogService;
        _groceryListService = groceryListService;
        Products = new ObservableCollection<Product>();

        AddProductCommand = new Command(async () => await Shell.Current.GoToAsync("add-product"));
        ViewProductCommand = new Command<Product>(async product => await ViewProductAsync(product));
        AddToListCommand = new Command<Product>(async product => await AddToListAsync(product));
        CompareCommand = new Command<Product>(async product => await CompareAsync(product));

        ApplyFilter();
        _catalogService.Products.CollectionChanged += OnCatalogChanged;
    }

    public ObservableCollection<Product> Products { get; }

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (SetProperty(ref _searchText, value))
            {
                ApplyFilter();
            }
        }
    }

    public Command AddProductCommand { get; }
    public Command<Product> ViewProductCommand { get; }
    public Command<Product> AddToListCommand { get; }
    public Command<Product> CompareCommand { get; }

    private void OnCatalogChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ApplyFilter();
    }

    private void ApplyFilter()
    {
        var filter = SearchText?.Trim();
        Products.Clear();

        IEnumerable<Product> result = _catalogService.Products;
        if (!string.IsNullOrWhiteSpace(filter))
        {
            result = result.Where(product =>
                product.Name.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                product.Category.Contains(filter, StringComparison.OrdinalIgnoreCase) ||
                product.Store.Contains(filter, StringComparison.OrdinalIgnoreCase));
        }

        foreach (var product in result)
        {
            Products.Add(product);
        }
    }

    private async Task ViewProductAsync(Product? product)
    {
        if (product is null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"product-detail?id={product.Id}");
    }

    private async Task AddToListAsync(Product? product)
    {
        if (product is null)
        {
            return;
        }

        _groceryListService.AddProduct(product);
        await UserFeedbackService.ShowSuccessAsync($"{product.Name} added to your grocery list.");
    }

    private async Task CompareAsync(Product? product)
    {
        if (product is null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"price-compare?id={product.Id}");
    }
}
