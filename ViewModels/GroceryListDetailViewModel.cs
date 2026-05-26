using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public class GroceryListItemDisplay : ObservableObject
    {
        public GroceryListItem Item { get; }
        public string ProductName { get; }
        public string Brand { get; }
        public double Price { get; }

        public GroceryListItemDisplay(GroceryListItem item, Product? product)
        {
            Item = item;
            ProductName = product?.Name ?? "Unknown product";
            Brand = product?.Brand ?? string.Empty;
            Price = product?.Price ?? 0;
        }

        public bool IsPurchased
        {
            get => Item.IsPurchased;
            set
            {
                if (Item.IsPurchased == value) return;
                Item.IsPurchased = value;
                OnPropertyChanged();
            }
        }

        public double Quantity => Item.Quantity;
        public string UnitOfMeasure => Item.UnitOfMeasure ?? string.Empty;
        public double LineTotal => Quantity * Price;
    }

    [QueryProperty(nameof(ListId), "listId")]
    public partial class GroceryListDetailViewModel : BaseViewModel
    {
        private readonly IGroceryListService _listService;
        private readonly IProductService _productService;

        private Dictionary<int, Product> _productLookup = new();
        private GroceryList? _list;

        public ObservableCollection<GroceryListItemDisplay> Items { get; } = new();
        public ObservableCollection<Product> AvailableProducts { get; } = new();

        [ObservableProperty] private int listId;
        [ObservableProperty] private string? listName;
        [ObservableProperty] private Product? selectedProduct;
        [ObservableProperty] private double newQuantity = 1;
        [ObservableProperty] private int totalItems;
        [ObservableProperty] private int purchasedCount;
        [ObservableProperty] private double totalPrice;

        public GroceryListDetailViewModel(IGroceryListService listService, IProductService productService)
        {
            Title = "List Detail";
            _listService = listService;
            _productService = productService;
        }

        partial void OnListIdChanged(int value)
        {
            _ = ReloadAsync();
        }

        [RelayCommand]
        public async Task ReloadAsync()
        {
            if (IsBusy || ListId == 0) return;
            try
            {
                IsBusy = true;
                _list = await _listService.GetListByIdAsync(ListId);
                ListName = _list?.Name;
                Title = _list?.Name ?? "List";

                var products = await _productService.GetProductsAsync();
                _productLookup = products.ToDictionary(p => p.Id);

                AvailableProducts.Clear();
                foreach (var p in products.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase))
                    AvailableProducts.Add(p);

                var items = await _listService.GetItemsForListAsync(ListId);
                Items.Clear();
                foreach (var it in items)
                {
                    _productLookup.TryGetValue(it.ProductId, out var prod);
                    Items.Add(new GroceryListItemDisplay(it, prod));
                }

                RefreshTotals();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void RefreshTotals()
        {
            TotalItems = Items.Count;
            PurchasedCount = Items.Count(i => i.IsPurchased);
            TotalPrice = Items.Sum(i => i.LineTotal);
        }

        [RelayCommand]
        private async Task AddItemAsync()
        {
            if (SelectedProduct == null)
            {
                await Shell.Current.DisplayAlert("Validation", "Pick a product to add.", "OK");
                return;
            }

            var item = new GroceryListItem
            {
                GroceryListId = ListId,
                ProductId = SelectedProduct.Id,
                Quantity = NewQuantity <= 0 ? 1 : NewQuantity,
                UnitOfMeasure = SelectedProduct.UnitOfMeasure ?? string.Empty,
                IsPurchased = false
            };
            await _listService.AddItemAsync(item);
            SelectedProduct = null;
            NewQuantity = 1;
            await ReloadAsync();
        }

        [RelayCommand]
        private async Task TogglePurchasedAsync(GroceryListItemDisplay display)
        {
            if (display == null) return;
            display.IsPurchased = !display.IsPurchased;
            await _listService.UpdateItemAsync(display.Item);
            PurchasedCount = Items.Count(i => i.IsPurchased);
        }

        [RelayCommand]
        private async Task DeleteItemAsync(GroceryListItemDisplay display)
        {
            if (display == null) return;
            await _listService.DeleteItemAsync(display.Item);
            await ReloadAsync();
        }
    }
}
