using System.Collections.ObjectModel;
using GroceryMate.Models;

namespace GroceryMate.Services;

public class GroceryListService
{
    public ObservableCollection<GroceryItem> Items { get; } = new();

    public void AddProduct(Product product)
    {
        var existing = Items.FirstOrDefault(item => item.Product.Id == product.Id);
        if (existing is not null)
        {
            existing.Quantity += 1;
            return;
        }

        Items.Add(new GroceryItem
        {
            Product = product,
            Quantity = 1,
            IsChecked = false
        });
    }

    public void Remove(GroceryItem item)
    {
        Items.Remove(item);
    }

    public void Toggle(GroceryItem item)
    {
        item.IsChecked = !item.IsChecked;
    }

    public void Increase(GroceryItem item)
    {
        item.Quantity += 1;
    }

    public void Decrease(GroceryItem item)
    {
        if (item.Quantity > 1)
        {
            item.Quantity -= 1;
        }
    }
}
