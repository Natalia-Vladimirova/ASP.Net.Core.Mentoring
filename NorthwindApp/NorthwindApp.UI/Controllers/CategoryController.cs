using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = (await _categoryService.GetCategoriesAsync())
                .Select(_mapper.Map<CategoryViewModel>);

            return View(categories);
        }

        public async Task<IActionResult> Image(int id)
        {
            var image = await _categoryService.GetCategoryImageAsync(id);
            
            if (image == null)
            {
                return NotFound();
            }

            return File(image, "image/*");
        }

        [HttpGet]
        public ActionResult UploadImage(int categoryId)
        {
            return View(new CategoryImageDetailsViewModel { CategoryId = categoryId });
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage(CategoryImageDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var memoryStream = new MemoryStream())
            {
                await model.Picture.CopyToAsync(memoryStream);
                await _categoryService.UploadCategoryImageAsync(model.CategoryId, memoryStream.ToArray());
            }

            return RedirectToAction("Index");
        }
    }
}
