using SmartGroceryList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGroceryList.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetFavoritesAsync();
        Task<List<Product>> GetFavoriteProductsAsync();
        Task AddFavoriteAsync(int productId);
        Task RemoveFavoriteAsync(Favorite favorite);
        Task<bool> IsFavoriteAsync(int productId);
    }
}
