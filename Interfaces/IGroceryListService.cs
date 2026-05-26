using SmartGroceryList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGroceryList.Interfaces
{
    public interface IGroceryListService
    {
        Task<List<GroceryList>> GetListsAsync();
        Task<GroceryList?> GetListByIdAsync(int id);
        Task AddListAsync(GroceryList list);
        Task UpdateListAsync(GroceryList list);
        Task DeleteListAsync(GroceryList list);

        Task<List<GroceryListItem>> GetItemsForListAsync(int groceryListId);
        Task AddItemAsync(GroceryListItem item);
        Task UpdateItemAsync(GroceryListItem item);
        Task DeleteItemAsync(GroceryListItem item);
    }
}
