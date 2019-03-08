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
            var products = await GetProductQuery().ToListAsync();

            return products.Select(_mapper.Map<Product>);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int count)
        {
            var products = await GetProductQuery()
                .Take(count)
                .ToListAsync();
            
            return products.Select(_mapper.Map<Product>);
        }

        private IQueryable<ProductDto> GetProductQuery()
        {
            return _context.Products
                .Include(x => x.Category)
                .Include(x => x.Supplier);
        }
    }
}
