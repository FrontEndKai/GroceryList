using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SQLite;
using System;
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
            return _db.QueryAsync<Category>("SELECT * FROM Category");
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
            // Keep this: We still want to build the database tables on startup
            await _db.CreateTableAsync<Category>();
            await _db.CreateTableAsync<Product>();
    
            // Keep this: We still want the category options (Fruits, Dairy, etc.) available for when users create items
            var existingCategories = await _db.Table<Category>().ToListAsync();
            if (existingCategories.Count == 0)
            {
                var seedCategories = new List<Category>
                {
                    new Category { Id = 1, Name = "Fruits & Vegetables" },
                    new Category { Id = 2, Name = "Dairy & Eggs" },
                    new Category { Id = 3, Name = "Bakery & Bread" },
                    new Category { Id = 4, Name = "Beverages" },
                    new Category { Id = 5, Name = "Pantry Essentials" },
                    new Category { Id = 6, Name = "Grains & Rice" },
                    new Category { Id = 7, Name = "Coffee & Tea" },
                    new Category { Id = 8, Name = "Snacks & Candies" },
                    new Category { Id = 9, Name = "Meat & Poultry" },
                    new Category { Id = 10, Name = "Household Items" },
                    new Category { Id = 11, Name = "Personal Care" },
                    new Category { Id = 12, Name = "Cleaning Supplies" },
                    new Category { Id = 13, Name = "Pet Care" }
                };
                await _db.InsertAllAsync(seedCategories);
            }
        }
    }
}