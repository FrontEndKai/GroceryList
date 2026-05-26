using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.Services
{
    public class ProductService : IProductService
    {
        private readonly SQLiteAsyncConnection _db;

        public ProductService(IDatabaseService databaseService)
        {
            _db = databaseService.GetConnection();
        }

        public Task<List<Product>> GetProductsAsync()
        {
            return _db.Table<Product>().ToListAsync();
        }

        public Task<Product?> GetProductByIdAsync(int id)
        {
            return _db.Table<Product>().FirstOrDefaultAsync(p => p.Id == id)!;
        }

        public Task<List<Category>> GetCategoriesAsync()
        {
            return _db.Table<Category>().ToListAsync();
        }

        public Task AddProductAsync(Product product)
        {
            return _db.InsertAsync(product);
        }

        public Task UpdateProductAsync(Product product)
        {
            return _db.UpdateAsync(product);
        }

        public Task DeleteProductAsync(Product product)
        {
            return _db.DeleteAsync(product);
        }

        public async Task SeedProductsAsync()
        {
            var seedProducts = new List<Product>
            {
                
            };

            var existingProducts = await _db.Table<Product>().ToListAsync();
            var seedByName = seedProducts.ToDictionary(p => p.Name);

            foreach (var existing in existingProducts)
            {
                if (!seedByName.TryGetValue(existing.Name, out var seed)) continue;

                var changed = false;
                if (existing.Price <= 0)
                {
                    existing.Price = seed.Price;
                    changed = true;
                }
                if (string.IsNullOrWhiteSpace(existing.Location))
                {
                    existing.Location = seed.Location;
                    changed = true;
                }
                if (string.IsNullOrWhiteSpace(existing.UnitOfMeasure))
                {
                    existing.UnitOfMeasure = seed.UnitOfMeasure;
                    changed = true;
                }

                if (changed) await _db.UpdateAsync(existing);
            }

            var existingNames = existingProducts.Select(product => product.Name).ToHashSet();
            var missingProducts = seedProducts.Where(product => !existingNames.Contains(product.Name)).ToList();

            if (missingProducts.Count > 0)
            {
                await _db.InsertAllAsync(missingProducts);
            }
        }
    }
}
