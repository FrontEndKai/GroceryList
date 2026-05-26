using SmartGroceryList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartGroceryList.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Category>> GetCategoriesAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task SeedProductsAsync();
    }
}
