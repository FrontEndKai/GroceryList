using SmartGroceryList.Interfaces;
using SmartGroceryList.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGroceryList.Services
{
    public class GroceryListService : IGroceryListService
    {
        private readonly SQLiteAsyncConnection _db;

        public GroceryListService(IDatabaseService databaseService)
        {
            _db = databaseService.GetConnection();
        }

        public Task<List<GroceryList>> GetListsAsync() =>
            _db.Table<GroceryList>().ToListAsync();

        public Task<GroceryList?> GetListByIdAsync(int id) =>
            _db.Table<GroceryList>().FirstOrDefaultAsync(l => l.Id == id)!;

        public Task AddListAsync(GroceryList list) => _db.InsertAsync(list);

        public Task UpdateListAsync(GroceryList list) => _db.UpdateAsync(list);

        public async Task DeleteListAsync(GroceryList list)
        {
            await _db.Table<GroceryListItem>().DeleteAsync(i => i.GroceryListId == list.Id);
            await _db.DeleteAsync(list);
        }

        public Task<List<GroceryListItem>> GetItemsForListAsync(int groceryListId) =>
            _db.Table<GroceryListItem>().Where(i => i.GroceryListId == groceryListId).ToListAsync();

        public Task AddItemAsync(GroceryListItem item) => _db.InsertAsync(item);

        public Task UpdateItemAsync(GroceryListItem item) => _db.UpdateAsync(item);

        public Task DeleteItemAsync(GroceryListItem item) => _db.DeleteAsync(item);
    }
}
