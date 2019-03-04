using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
