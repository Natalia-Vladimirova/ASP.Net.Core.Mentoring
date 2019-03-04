using Microsoft.AspNetCore.Mvc;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            var categories = new[]
            {
                new CategoryViewModel
                {
                    CategoryId = 1,
                    CategoryName = "Beverages",
                    Description = "Soft drinks, coffees, teas, beers, and ales"
                }
            };

            return View(categories);
        }
    }
}
