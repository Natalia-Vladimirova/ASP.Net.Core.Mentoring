using Microsoft.AspNetCore.Mvc;

namespace NorthwindApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
