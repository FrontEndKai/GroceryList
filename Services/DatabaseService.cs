using SQLite;
using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartGroceryList.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _database;
        private const string DatabaseFilename = "SmartGrocery.db3";

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
            
            // Seed initial data if needed
            await SeedData(db);
        }

        private async Task SeedData(SQLiteAsyncConnection db)
        {
            if (await db.Table<Category>().CountAsync() == 0)
            {
                await db.InsertAllAsync(new Category[]
                {
                    new Category { Name = "Fruits" },
                    new Category { Name = "Vegetables" },
                    new Category { Name = "Meat & Poultry" },
                    new Category { Name = "Dairy & Eggs" },
                    new Category { Name = "Bakery" },
                    new Category { Name = "Pantry" },
                    new Category { Name = "Beverages" },
                    new Category { Name = "Snacks" }
                });
            }
        }
    }
}
