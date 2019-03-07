using System.Linq;
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

        public IActionResult Index()
        {
            var categories = _categoryService.GetCategories()
                .Select(_mapper.Map<CategoryViewModel>);

            return View(categories);
        }
    }
}
