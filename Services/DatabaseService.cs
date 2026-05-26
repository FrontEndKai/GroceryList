using SQLite;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGroceryList.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _database;
        private const string DatabaseFilename = "SmartGrocery.db3";

        private static readonly string[] SeedCategoryNames =
        {
            "Fruits",
            "Vegetables",
            "Meat & Poultry",
            "Dairy & Eggs",
            "Bakery",
            "Pantry",
            "Beverages",
            "Snacks",
            "Household",
            "Personal Care",
            "Cleaning Supplies",
            "Pet Supplies",
            "Other"
        };

        private static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        public SQLiteAsyncConnection GetConnection()
        {
            if (_database == null)
            {
                _database = new SQLiteAsyncConnection(DatabasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache);
            }
            return _database;
        }

        public async Task Init()
        {
            var db = GetConnection();
            await db.CreateTableAsync<User>();
            await db.CreateTableAsync<Product>();
            await db.CreateTableAsync<Category>();
            await db.CreateTableAsync<GroceryList>();
            await db.CreateTableAsync<GroceryListItem>();
            await db.CreateTableAsync<PurchaseHistory>();
            await db.CreateTableAsync<Store>();
            await db.CreateTableAsync<Favorite>();

            await SeedData(db);
        }

        private async Task SeedData(SQLiteAsyncConnection db)
        {
            var existing = await db.Table<Category>().ToListAsync();
            var existingNames = existing.Select(c => c.Name).ToHashSet();

            var missing = SeedCategoryNames
                .Where(name => !existingNames.Contains(name))
                .Select(name => new Category { Name = name })
                .ToList();

            if (missing.Count > 0)
            {
                await db.InsertAllAsync(missing);
            }
        }
    }
}
