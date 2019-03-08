using System.Collections.Generic;
using System.Threading.Tasks;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }
    }
}
