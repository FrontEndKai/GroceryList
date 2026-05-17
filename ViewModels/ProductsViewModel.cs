using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class ProductsViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private readonly List<Product> _allProducts = new();
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
                    (product.Description?.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();

            Products.Clear();
            foreach (var product in filtered)
            {
                Products.Add(product);
            }

            ProductCount = Products.Count;
        }
    }
}
