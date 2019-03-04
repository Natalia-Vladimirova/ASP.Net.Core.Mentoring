using Microsoft.AspNetCore.Mvc;

namespace NorthwindApp.UI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
