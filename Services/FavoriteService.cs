using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly SQLiteAsyncConnection _db;

        public FavoriteService(IDatabaseService databaseService)
        {
            _db = databaseService.GetConnection();
        }

        public Task<List<Favorite>> GetFavoritesAsync() =>
            _db.Table<Favorite>().ToListAsync();

        public async Task<List<Product>> GetFavoriteProductsAsync()
        {
            var favs = await _db.Table<Favorite>().ToListAsync();
            if (favs.Count == 0)
                return new List<Product>();

            var ids = favs.Select(f => f.ProductId).ToHashSet();
            var allProducts = await _db.Table<Product>().ToListAsync();
            return allProducts.Where(p => ids.Contains(p.Id)).ToList();
        }

        public Task AddFavoriteAsync(int productId) =>
            _db.InsertAsync(new Favorite { ProductId = productId });

        public Task RemoveFavoriteAsync(Favorite favorite) =>
            _db.DeleteAsync(favorite);

        public async Task<bool> IsFavoriteAsync(int productId)
        {
            var match = await _db.Table<Favorite>().FirstOrDefaultAsync(f => f.ProductId == productId);
            return match != null;
        }
    }
}
