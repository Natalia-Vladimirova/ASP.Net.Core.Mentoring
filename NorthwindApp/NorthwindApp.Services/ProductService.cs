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

        public async Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize)
        {
            if (pageSize > 0)
            {
                var skip = (page - 1) * pageSize;

                return await _productRepository.GetProductsAsync(pageSize, skip);
            }

            return await _productRepository.GetProductsAsync();
        }

        public async Task<int> GetProductsCountAsync()
        {
            return await _productRepository.GetProductsCountAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _productRepository.GetProductAsync(id);
        }

        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        public async Task EditProductAsync(Product product)
        {
            await _productRepository.EditProductAsync(product);
        }
    }
}
