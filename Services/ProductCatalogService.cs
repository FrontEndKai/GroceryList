using System.Collections.ObjectModel;
using GroceryMate.Models;

namespace GroceryMate.Services;

public class ProductCatalogService
{
    private readonly Dictionary<string, List<PriceOption>> _priceOptions = new();

    public ObservableCollection<Product> Products { get; } = new();

    public IReadOnlyList<string> Categories { get; } = new List<string>
    {
        "Produce",
        "Dairy",
        "Bakery",
        "Meat",
        "Pantry",
        "Snacks",
        "Beverages"
    };

    public IReadOnlyList<string> Stores { get; } = new List<string>
    {
        "FreshMart",
        "GreenBasket",
        "MarketHub",
        "DailyHarvest"
    };

    public ProductCatalogService()
    {
        SeedProducts();
    }

    public Product? GetById(string id)
    {
        return Products.FirstOrDefault(p => p.Id == id);
    }

    public void AddProduct(Product product)
    {
        Products.Insert(0, product);
        _priceOptions[product.Id] = BuildPriceOptions(product);
    }

    public IReadOnlyList<PriceOption> GetPriceOptions(string productId)
    {
        if (_priceOptions.TryGetValue(productId, out var options))
        {
            return options;
        }

        var product = GetById(productId);
        if (product is null)
        {
            return Array.Empty<PriceOption>();
        }

        var created = BuildPriceOptions(product);
        _priceOptions[product.Id] = created;
        return created;
    }

    private void SeedProducts()
    {
        var items = new List<Product>
        {
            new()
            {
                Name = "Honeycrisp Apples",
                Category = "Produce",
                Price = 2.49m,
                Store = "FreshMart",
                Unit = "per lb",
                Description = "Crisp, sweet apples for snacks or pies.",
                Aisle = "Aisle 1"
            },
            new()
            {
                Name = "Almond Milk",
                Category = "Dairy",
                Price = 3.79m,
                Store = "GreenBasket",
                Unit = "1L",
                Description = "Unsweetened, dairy-free almond milk.",
                Aisle = "Aisle 4"
            },
            new()
            {
                Name = "Sourdough Loaf",
                Category = "Bakery",
                Price = 4.25m,
                Store = "MarketHub",
                Unit = "1 loaf",
                Description = "Artisan sourdough with a crunchy crust.",
                Aisle = "Bakery"
            },
            new()
            {
                Name = "Free Range Eggs",
                Category = "Dairy",
                Price = 3.99m,
                Store = "DailyHarvest",
                Unit = "12 pack",
                Description = "Omega-3 enriched eggs.",
                Aisle = "Aisle 3"
            },
            new()
            {
                Name = "Penne Pasta",
                Category = "Pantry",
                Price = 1.69m,
                Store = "FreshMart",
                Unit = "500g",
                Description = "Bronze-cut pasta with great texture.",
                Aisle = "Aisle 8"
            },
            new()
            {
                Name = "Olive Oil",
                Category = "Pantry",
                Price = 8.49m,
                Store = "GreenBasket",
                Unit = "500ml",
                Description = "Extra virgin olive oil for salads and cooking.",
                Aisle = "Aisle 9"
            },
            new()
            {
                Name = "Chicken Breast",
                Category = "Meat",
                Price = 6.75m,
                Store = "MarketHub",
                Unit = "1 lb",
                Description = "Lean, boneless chicken breast.",
                Aisle = "Butcher"
            },
            new()
            {
                Name = "Granola Mix",
                Category = "Snacks",
                Price = 5.39m,
                Store = "DailyHarvest",
                Unit = "400g",
                Description = "Crunchy granola with seeds and berries.",
                Aisle = "Aisle 6"
            },
            new()
            {
                Name = "Sparkling Water",
                Category = "Beverages",
                Price = 4.10m,
                Store = "FreshMart",
                Unit = "6 pack",
                Description = "Lime flavored sparkling water.",
                Aisle = "Aisle 10"
            }
        };

        foreach (var item in items)
        {
            Products.Add(item);
            _priceOptions[item.Id] = BuildPriceOptions(item);
        }
    }

    private List<PriceOption> BuildPriceOptions(Product product)
    {
        var basePrice = product.Price;
        var options = new List<PriceOption>
        {
            new()
            {
                Store = "FreshMart",
                Price = Math.Round(basePrice * 0.96m, 2),
                Note = "Loyalty savings"
            },
            new()
            {
                Store = "GreenBasket",
                Price = Math.Round(basePrice * 1.03m, 2),
                Note = "Organic supplier"
            },
            new()
            {
                Store = "MarketHub",
                Price = Math.Round(basePrice * 0.92m, 2),
                Note = "Weekly deal"
            },
            new()
            {
                Store = "DailyHarvest",
                Price = Math.Round(basePrice * 1.01m, 2),
                Note = "Same-day delivery"
            }
        };

        var best = options.MinBy(option => option.Price);
        if (best is not null)
        {
            best.IsBest = true;
        }

        return options;
    }
}
