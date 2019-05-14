using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthwindApp.DAL.Interfaces;
using NorthwindApp.Models;
using NorthwindApp.Services.Configuration;
using NorthwindApp.Services.Interfaces;

namespace NorthwindApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryImageOptions _categoryImageOptions;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IOptions<CategoryImageOptions> categoryImageOptions)
        {
            _categoryRepository = categoryRepository;
            _categoryImageOptions = categoryImageOptions.Value;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }

        public async Task<byte[]> GetCategoryImageAsync(int id)
        {
            var brokenImage = await _categoryRepository.GetCategoryImageAsync(id);

            return brokenImage?.Skip(_categoryImageOptions.CategoryImageGarbageSize).ToArray();
        }

        public async Task UploadCategoryImageAsync(int id, byte[] image)
        {
            var garbage = Enumerable.Range(0, _categoryImageOptions.CategoryImageGarbageSize)
                .Select(x => (byte)0);

            var brokenImage = new List<byte>(garbage);
            brokenImage.AddRange(image);

            await _categoryRepository.UploadCategoryImageAsync(id, brokenImage.ToArray());
        }
    }
}
