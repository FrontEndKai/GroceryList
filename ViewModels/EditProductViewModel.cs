using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    [QueryProperty(nameof(ProductId), "productId")]
    public partial class EditProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;
        private Product? _product;

        public ObservableCollection<Category> Categories { get; } = new();

        [ObservableProperty] private int productId;
        [ObservableProperty] private string? name;
        [ObservableProperty] private string? brand;
        [ObservableProperty] private string? description;
        [ObservableProperty] private double price;
        [ObservableProperty] private string? location;
        [ObservableProperty] private string? unitOfMeasure;
        [ObservableProperty] private Category? selectedCategory;

        public EditProductViewModel(IProductService productService)
        {
            Title = "Edit Product";
            _productService = productService;
        }

        partial void OnProductIdChanged(int value)
        {
            _ = LoadAsync();
        }

        public async Task LoadAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Categories.Clear();
                foreach (var c in await _productService.GetCategoriesAsync())
                    Categories.Add(c);

                _product = await _productService.GetProductByIdAsync(ProductId);
                if (_product == null) return;

                Name = _product.Name;
                Brand = _product.Brand;
                Description = _product.Description;
                Price = _product.Price;
                Location = _product.Location;
                UnitOfMeasure = _product.UnitOfMeasure;
                SelectedCategory = Categories.FirstOrDefault(c => c.Id == _product.CategoryId);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (_product == null || IsBusy) return;

            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Validation", "Please enter a product name.", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                _product.Name = Name!;
                _product.Brand = Brand ?? string.Empty;
                _product.Description = Description ?? string.Empty;
                _product.Price = Price < 0 ? 0 : Price;
                _product.Location = Location ?? string.Empty;
                _product.UnitOfMeasure = UnitOfMeasure ?? string.Empty;
                _product.CategoryId = SelectedCategory?.Id ?? _product.CategoryId;

                await _productService.UpdateProductAsync(_product);
                await Shell.Current.GoToAsync("..");
            }
            catch (System.Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (_product == null || IsBusy) return;

            var confirm = await Shell.Current.DisplayAlert(
                "Delete product",
                $"Remove {_product.Name}? This cannot be undone.",
                "Delete",
                "Cancel");

            if (!confirm) return;

            try
            {
                IsBusy = true;
                await _productService.DeleteProductAsync(_product);
                await Shell.Current.GoToAsync("..");
            }
            catch (System.Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private Task CancelAsync() => Shell.Current.GoToAsync("..");
    }
}
