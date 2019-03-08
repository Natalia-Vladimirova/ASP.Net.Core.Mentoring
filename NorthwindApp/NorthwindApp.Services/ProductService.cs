using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IConfigurationProvider _configurationProvider;

        public ProductService(
            IProductRepository productRepository,
            IConfigurationProvider configurationProvider)
        {
            _productRepository = productRepository;
            _configurationProvider = configurationProvider;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var productsCount = _configurationProvider.ProductPageSize;

            if (productsCount > 0)
            {
                return await _productRepository.GetProductsAsync(productsCount);
            }

            return await _productRepository.GetProductsAsync();
        }
    }
}
