using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NorthwindApp.DAL.Infrastructure;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(NorthwindDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.Select(_mapper.Map<Category>);
        }
    }
}
