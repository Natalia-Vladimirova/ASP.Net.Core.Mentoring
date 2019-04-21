using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<IEnumerable<Product>> GetProductsAsync(int take, int skip);

        Task<int> GetProductsCountAsync();

        Task<Product> GetProductAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task EditProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}
