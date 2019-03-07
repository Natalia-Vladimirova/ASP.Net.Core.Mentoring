using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.Select(_mapper.Map<Product>);
        }
    }
}
