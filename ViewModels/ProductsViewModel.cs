using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SmartGroceryList.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class ProductsViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly List<Product> _allProducts = new();
        private readonly Dictionary<int, string> _categoryLookup = new();
        public ObservableCollection<Product> Products { get; } = new();

        [ObservableProperty]
        private int productCount;

        [ObservableProperty]
        private string? searchText;

        public ProductsViewModel(IProductService productService)
        {
            Title = "Dashboard";
            _productService = productService;
        }

        [RelayCommand]
        async Task GetProductsAsync()
        {
            await LoadProductsAsync(resetSearch: false);
        }

        [RelayCommand]
        Task GoToAddProductAsync() =>
            Shell.Current.GoToAsync(nameof(AddProductPage));

        [RelayCommand]
        Task GoToEditProductAsync(Product product)
        {
            if (product == null) return Task.CompletedTask;
            return Shell.Current.GoToAsync($"{nameof(EditProductPage)}?productId={product.Id}");
        }

        public Task ReloadProductsAsync()
        {
            return LoadProductsAsync(resetSearch: true);
        }

        private async Task LoadProductsAsync(bool resetSearch)
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                await _productService.SeedProductsAsync(); // Seed sample data
                
                var cats = await _productService.GetCategoriesAsync();
                _categoryLookup.Clear();
                foreach (var c in cats)
                {
                    if (c != null)
                        _categoryLookup[c.Id] = c.Name ?? string.Empty;
                }

                var products = await _productService.GetProductsAsync();

                _allProducts.Clear();
                _allProducts.AddRange(products);

                if (resetSearch)
                {
                    if (!string.IsNullOrWhiteSpace(SearchText))
                    {
                        SearchText = string.Empty;
                    }
                    else
                    {
                        ApplySearchFilter();
                    }
                }
                else
                {
                    ApplySearchFilter();
                }
            }
            catch (System.Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Could not get products: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        partial void OnSearchTextChanged(string? value)
        {
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allProducts
                : _allProducts.Where(product =>
                        (product.Name?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (product.Brand?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (product.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false) ||
                        (_categoryLookup.TryGetValue(product.CategoryId, out var catName) && 
                         catName.Contains(SearchText, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

            Products.Clear();
            foreach (var product in filtered.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase))
            {
                Products.Add(product);
            }

            ProductCount = Products.Count;
        }
    }
}
