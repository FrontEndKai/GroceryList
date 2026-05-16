using GroceryMate.Models;
using GroceryMate.Services;

namespace GroceryMate.ViewModels;

public class AddProductViewModel : BaseViewModel
{
    private readonly ProductCatalogService _catalogService;
    private string _name = string.Empty;
    private string _category = string.Empty;
    private string _store = string.Empty;
    private string _priceText = string.Empty;
    private string _unit = string.Empty;
    private string _description = string.Empty;
    private string _statusMessage = string.Empty;

    public AddProductViewModel(ProductCatalogService catalogService)
    {
        _catalogService = catalogService;
        Categories = catalogService.Categories;
        Stores = catalogService.Stores;
        Category = Categories.FirstOrDefault() ?? string.Empty;
        Store = Stores.FirstOrDefault() ?? string.Empty;

        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public IReadOnlyList<string> Categories { get; }
    public IReadOnlyList<string> Stores { get; }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public string Category
    {
        get => _category;
        set => SetProperty(ref _category, value);
    }

    public string Store
    {
        get => _store;
        set => SetProperty(ref _store, value);
    }

    public string PriceText
    {
        get => _priceText;
        set => SetProperty(ref _priceText, value);
    }

    public string Unit
    {
        get => _unit;
        set => SetProperty(ref _unit, value);
    }

    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            if (SetProperty(ref _statusMessage, value))
            {
                OnPropertyChanged(nameof(HasStatus));
            }
        }
    }

    public bool HasStatus => !string.IsNullOrWhiteSpace(StatusMessage);

    public Command SaveCommand { get; }
    public Command CancelCommand { get; }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            StatusMessage = "Product name is required.";
            return;
        }

        if (!decimal.TryParse(PriceText, out var price) || price <= 0)
        {
            StatusMessage = "Enter a valid price.";
            return;
        }

        var product = new Product
        {
            Name = Name.Trim(),
            Category = Category,
            Store = Store,
            Price = price,
            Unit = string.IsNullOrWhiteSpace(Unit) ? "1 unit" : Unit.Trim(),
            Description = string.IsNullOrWhiteSpace(Description)
                ? "Freshly added product."
                : Description.Trim(),
            Aisle = "New Arrivals"
        };

        _catalogService.AddProduct(product);
        StatusMessage = string.Empty;
        await UserFeedbackService.ShowSuccessAsync($"{product.Name} added to the catalog.");
        await Shell.Current.GoToAsync("..");
    }
}
