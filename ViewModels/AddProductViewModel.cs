using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class AddProductViewModel : BaseViewModel
    {
        private readonly IProductService _productService;

        public ObservableCollection<Category> Categories { get; } = new();

        [ObservableProperty] private string? name;
        [ObservableProperty] private string? brand;
        [ObservableProperty] private string? description;
        [ObservableProperty] private double quantity = 1;
        [ObservableProperty] private string? unitOfMeasure;
        [ObservableProperty] private Category? selectedCategory;

        public AddProductViewModel(IProductService productService)
        {
            Title = "Add Product";
            _productService = productService;
        }

        public async Task LoadCategoriesAsync()
        {
            Categories.Clear();
            var cats = await _productService.GetCategoriesAsync();
            foreach (var c in cats)
                Categories.Add(c);
        }

        [RelayCommand]
        private async Task SaveAsync()
        {
            if (IsBusy) return;

            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Validation", "Please enter a product name.", "OK");
                return;
            }

            try
            {
                IsBusy = true;
                var product = new Product
                {
                    Name = Name!,
                    Brand = Brand ?? string.Empty,
                    Description = Description ?? string.Empty,
                    Quantity = Quantity,
                    UnitOfMeasure = UnitOfMeasure ?? string.Empty,
                    CategoryId = SelectedCategory?.Id ?? 0,
                    ImageUrl = string.Empty
                };

                await _productService.AddProductAsync(product);
                await Shell.Current.DisplayAlert("Saved", $"{product.Name} added.", "OK");
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
