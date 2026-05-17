using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class ProductsViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        public ObservableCollection<Product> Products { get; } = new();

        [ObservableProperty]
        private int productCount;

        public ProductsViewModel(IProductService productService)
        {
            Title = "Dashboard";
            _productService = productService;
        }

        [RelayCommand]
        async Task GetProductsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                await _productService.SeedProductsAsync(); // Seed sample data
                var products = await _productService.GetProductsAsync();

                if (Products.Count != 0)
                    Products.Clear();

                foreach (var product in products)
                    Products.Add(product);

                ProductCount = Products.Count;
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
    }
}
