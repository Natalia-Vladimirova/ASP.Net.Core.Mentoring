using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize);

        Task<int> GetProductsCountAsync();

        Task<Product> GetProductAsync(int id);

        Task<Product> AddProductAsync(Product product);

        Task EditProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}
