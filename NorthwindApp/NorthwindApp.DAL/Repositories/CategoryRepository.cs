using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.DAL.Entities;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;

namespace NorthwindApp.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Set<CategoryDto>().ToListAsync();

            return categories.Select(_mapper.Map<Category>);
        }

        public async Task<byte[]> GetCategoryImageAsync(int id)
        {
            var categoryDetails = await _context.Set<CategoryImageDetailsDto>()
                .FirstOrDefaultAsync(x => x.CategoryId == id);

            return categoryDetails?.Picture;
        }
    }
}
