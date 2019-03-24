using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.Services.Interfaces;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IMimeHelper _mimeHelper;

        public CategoryController(
            ICategoryService categoryService, 
            IMapper mapper, 
            IMimeHelper mimeHelper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _mimeHelper = mimeHelper;
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

            return File(image, _mimeHelper.GetMimeType(image));
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
