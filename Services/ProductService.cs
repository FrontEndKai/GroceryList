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
                new Product { Name = "Banana", Brand = "Local Farm", CategoryId = 1, ImageUrl = "banana.png", Description = "Fresh Cavendish bananas.", Quantity = 1, UnitOfMeasure = "bunch" },
                new Product { Name = "Whole Milk", Brand = "FreshMilk", CategoryId = 4, ImageUrl = "milk.png", Description = "1L full cream milk.", Quantity = 1, UnitOfMeasure = "carton" },
                new Product { Name = "Loaf Bread", Brand = "Gardenia", CategoryId = 5, ImageUrl = "bread.png", Description = "Classic white bread.", Quantity = 1, UnitOfMeasure = "loaf" },
                new Product { Name = "Chicken Breast", Brand = "Magnolia", CategoryId = 3, ImageUrl = "chicken.png", Description = "Skinless chicken breast fillet.", Quantity = 1, UnitOfMeasure = "pack" },
                new Product { Name = "Cheddar Cheese", Brand = "Eden", CategoryId = 4, ImageUrl = "cheese.png", Description = "Original cheddar cheese block.", Quantity = 1, UnitOfMeasure = "block" },
                new Product { Name = "Eggs", Brand = "Farm Fresh", CategoryId = 2, ImageUrl = "eggs.png", Description = "One dozen fresh eggs.", Quantity = 12, UnitOfMeasure = "pcs" },
                new Product { Name = "Apple", Brand = "Green Valley", CategoryId = 1, ImageUrl = "apple.png", Description = "Sweet and crisp apples.", Quantity = 1, UnitOfMeasure = "kg" },
                new Product { Name = "Rice", Brand = "Bounty", CategoryId = 5, ImageUrl = "rice.png", Description = "Premium white rice.", Quantity = 1, UnitOfMeasure = "sack" },
                new Product { Name = "Coffee", Brand = "Kopiko", CategoryId = 6, ImageUrl = "coffee.png", Description = "Instant coffee mix.", Quantity = 1, UnitOfMeasure = "pack" },
                new Product { Name = "Tomato", Brand = "Fresh Harvest", CategoryId = 1, ImageUrl = "tomato.png", Description = "Juicy ripe tomatoes.", Quantity = 1, UnitOfMeasure = "kg" },
                new Product { Name = "Butter", Brand = "Anchor", CategoryId = 4, ImageUrl = "butter.png", Description = "Creamy salted butter.", Quantity = 1, UnitOfMeasure = "pack" },
                new Product { Name = "Oats", Brand = "Quaker", CategoryId = 5, ImageUrl = "oats.png", Description = "Healthy rolled oats.", Quantity = 1, UnitOfMeasure = "pack" }
            };

            var existingProducts = await _db.Table<Product>().ToListAsync();
            var existingNames = existingProducts.Select(product => product.Name).ToHashSet();
            var missingProducts = seedProducts.Where(product => !existingNames.Contains(product.Name)).ToList();

            if (missingProducts.Count > 0)
            {
                await _db.InsertAllAsync(missingProducts);
            }
        }
    }
}
