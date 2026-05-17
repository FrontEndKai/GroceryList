using SmartGroceryList.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGroceryList.Interfaces
{
    public interface IDatabaseService
    {
        SQLiteAsyncConnection GetConnection();
        Task Init();
    }
}
