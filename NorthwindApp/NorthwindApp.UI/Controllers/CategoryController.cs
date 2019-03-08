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
    }
}
