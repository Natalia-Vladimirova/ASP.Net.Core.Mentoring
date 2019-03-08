using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Entities;
using NorthwindApp.DAL.Infrastructure;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await GetProducts(x => x);

            return products.Select(_mapper.Map<Product>);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int count)
        {
            var products = await GetProducts(x => x.Take(count));
            
            return products.Select(_mapper.Map<Product>);
        }

        public async Task AddProductAsync(Product product)
        {
            var productDto = _mapper.Map<ProductDto>(product);

            await _context.Products.AddAsync(productDto);
            await _context.SaveChangesAsync();
        }

        private async Task<IEnumerable<ProductDto>> GetProducts(
            Func<IQueryable<ProductDto>, IQueryable<ProductDto>> filterQuery)
        {
            return await filterQuery(_context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier))
                .ToListAsync();
        }
    }
}
