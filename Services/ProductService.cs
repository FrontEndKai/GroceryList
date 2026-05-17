using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SQLite;
using System.Collections.Generic;
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

        public Task AddProductAsync(Product product)
        {
            return _db.InsertAsync(product);
        }

        public async Task SeedProductsAsync()
        {
            if (await _db.Table<Product>().CountAsync() == 0)
            {
                var products = new List<Product>
                {
                    new Product { Name = "Banana", Brand = "Local Farm", CategoryId = 1, ImageUrl = "banana.png", Description = "Fresh Cavendish bananas." },
                    new Product { Name = "Whole Milk", Brand = "FreshMilk", CategoryId = 4, ImageUrl = "milk.png", Description = "1L Full cream milk." },
                    new Product { Name = "Loaf Bread", Brand = "Gardenia", CategoryId = 5, ImageUrl = "bread.png", Description = "Classic white bread." },
                    new Product { Name = "Chicken Breast", Brand = "Magnolia", CategoryId = 3, ImageUrl = "chicken.png", Description = "Skinless chicken breast fillet." },
                    new Product { Name = "Cheddar Cheese", Brand = "Eden", CategoryId = 4, ImageUrl = "cheese.png", Description = "Original cheddar cheese block." }
                };
                await _db.InsertAllAsync(products);
            }
        }
    }
}
