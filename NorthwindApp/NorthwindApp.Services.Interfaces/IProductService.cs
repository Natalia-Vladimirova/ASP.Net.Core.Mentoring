using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Models;

namespace NorthwindApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int count);

        Task<Product> GetProductAsync(int id);

        Task AddProductAsync(Product product);

        Task EditProductAsync(Product product);
    }
}
