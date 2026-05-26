using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.ViewModels
{
    public partial class FavoritesViewModel : BaseViewModel
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IProductService _productService;

        public ObservableCollection<Product> FavoriteProducts { get; } = new();
        public ObservableCollection<Product> AvailableProducts { get; } = new();

        [ObservableProperty] private Product? selectedProduct;
        [ObservableProperty] private int favoriteCount;

        public FavoritesViewModel(IFavoriteService favoriteService, IProductService productService)
        {
            Title = "Favorites";
            _favoriteService = favoriteService;
            _productService = productService;
        }

        [RelayCommand]
        public async Task ReloadAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;

                var products = await _productService.GetProductsAsync();
                AvailableProducts.Clear();
                foreach (var p in products)
                    AvailableProducts.Add(p);

                var favs = await _favoriteService.GetFavoriteProductsAsync();
                FavoriteProducts.Clear();
                foreach (var p in favs)
                    FavoriteProducts.Add(p);

                FavoriteCount = FavoriteProducts.Count;
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task AddFavoriteAsync()
        {
            if (SelectedProduct == null)
            {
                await Shell.Current.DisplayAlert("Validation", "Pick a product to favorite.", "OK");
                return;
            }

            if (await _favoriteService.IsFavoriteAsync(SelectedProduct.Id))
            {
                await Shell.Current.DisplayAlert("Already saved", $"{SelectedProduct.Name} is already in your favorites.", "OK");
                return;
            }

            await _favoriteService.AddFavoriteAsync(SelectedProduct.Id);
            SelectedProduct = null;
            await ReloadAsync();
        }

        [RelayCommand]
        private async Task RemoveFavoriteAsync(Product product)
        {
            if (product == null) return;

            var favorites = await _favoriteService.GetFavoritesAsync();
            var match = favorites.FirstOrDefault(f => f.ProductId == product.Id);
            if (match == null) return;

            await _favoriteService.RemoveFavoriteAsync(match);
            await ReloadAsync();
        }
    }
}
