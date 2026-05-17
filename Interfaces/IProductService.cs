using SmartGroceryList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGroceryList.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task AddProductAsync(Product product);
        Task SeedProductsAsync();
    }
}
