using Microsoft.AspNetCore.Mvc;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.ViewComponents
{
    public class BreadcrumbsViewComponent : ViewComponent
    {
        private readonly ISiteMapBuilder _siteMapBuilder;

        public BreadcrumbsViewComponent(ISiteMapBuilder siteMapBuilder)
        {
            _siteMapBuilder = siteMapBuilder;
        }

        public IViewComponentResult Invoke()
        {
            var controller = ViewContext.RouteData.Values["Controller"].ToString();
            var action = ViewContext.RouteData.Values["Action"].ToString();

            var breadcrumbs = _siteMapBuilder.Build(controller, action);

            return View(breadcrumbs);
        }
    }
}
