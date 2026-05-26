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
                new Product { Name = "Banana", Brand = "Local Farm", CategoryId = 1, ImageUrl = "banana.png", Description = "Fresh Cavendish bananas.", Price = 65.00, Location = "Produce Aisle", UnitOfMeasure = "bunch" },
                new Product { Name = "Whole Milk", Brand = "FreshMilk", CategoryId = 4, ImageUrl = "milk.png", Description = "1L full cream milk.", Price = 95.00, Location = "Dairy Section", UnitOfMeasure = "carton" },
                new Product { Name = "Loaf Bread", Brand = "Gardenia", CategoryId = 5, ImageUrl = "bread.png", Description = "Classic white bread.", Price = 75.00, Location = "Bakery Aisle", UnitOfMeasure = "loaf" },
                new Product { Name = "Chicken Breast", Brand = "Magnolia", CategoryId = 3, ImageUrl = "chicken.png", Description = "Skinless chicken breast fillet.", Price = 280.00, Location = "Meat Section", UnitOfMeasure = "pack" },
                new Product { Name = "Cheddar Cheese", Brand = "Eden", CategoryId = 4, ImageUrl = "cheese.png", Description = "Original cheddar cheese block.", Price = 165.00, Location = "Dairy Section", UnitOfMeasure = "block" },
                new Product { Name = "Eggs", Brand = "Farm Fresh", CategoryId = 2, ImageUrl = "eggs.png", Description = "One dozen fresh eggs.", Price = 110.00, Location = "Dairy Section", UnitOfMeasure = "dozen" },
                new Product { Name = "Apple", Brand = "Green Valley", CategoryId = 1, ImageUrl = "apple.png", Description = "Sweet and crisp apples.", Price = 180.00, Location = "Produce Aisle", UnitOfMeasure = "kg" },
                new Product { Name = "Rice", Brand = "Bounty", CategoryId = 6, ImageUrl = "rice.png", Description = "Premium white rice.", Price = 320.00, Location = "Pantry Aisle", UnitOfMeasure = "sack" },
                new Product { Name = "Coffee", Brand = "Kopiko", CategoryId = 7, ImageUrl = "coffee.png", Description = "Instant coffee mix.", Price = 130.00, Location = "Beverages Aisle", UnitOfMeasure = "pack" },
                new Product { Name = "Tomato", Brand = "Fresh Harvest", CategoryId = 2, ImageUrl = "tomato.png", Description = "Juicy ripe tomatoes.", Price = 90.00, Location = "Produce Aisle", UnitOfMeasure = "kg" },
                new Product { Name = "Butter", Brand = "Anchor", CategoryId = 4, ImageUrl = "butter.png", Description = "Creamy salted butter.", Price = 145.00, Location = "Dairy Section", UnitOfMeasure = "pack" },
                new Product { Name = "Oats", Brand = "Quaker", CategoryId = 6, ImageUrl = "oats.png", Description = "Healthy rolled oats.", Price = 210.00, Location = "Pantry Aisle", UnitOfMeasure = "pack" },
                new Product { Name = "Dish Soap", Brand = "Joy", CategoryId = 11, ImageUrl = "dishsoap.png", Description = "Lemon-scented dishwashing liquid.", Price = 95.00, Location = "Cleaning Aisle", UnitOfMeasure = "bottle" },
                new Product { Name = "Shampoo", Brand = "Sunsilk", CategoryId = 10, ImageUrl = "shampoo.png", Description = "Smooth and manageable hair shampoo.", Price = 165.00, Location = "Personal Care Aisle", UnitOfMeasure = "bottle" },
                new Product { Name = "Toilet Paper", Brand = "Charmin", CategoryId = 9, ImageUrl = "tissue.png", Description = "Soft 2-ply toilet tissue, 4 rolls.", Price = 120.00, Location = "Household Aisle", UnitOfMeasure = "pack" },
                new Product { Name = "Dog Food", Brand = "Pedigree", CategoryId = 12, ImageUrl = "dogfood.png", Description = "Adult dog dry food.", Price = 285.00, Location = "Pet Care Aisle", UnitOfMeasure = "bag" }
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
