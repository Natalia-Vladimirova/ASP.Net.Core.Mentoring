using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int count)
        {
            if (count > 0)
            {
                return await _productRepository.GetProductsAsync(count);
            }

            return await _productRepository.GetProductsAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }
    }
}
