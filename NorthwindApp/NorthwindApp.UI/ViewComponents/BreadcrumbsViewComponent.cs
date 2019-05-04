using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.UI.Interfaces;
using NorthwindApp.UI.Models;

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
            var controller = ViewContext.RouteData.Values["Controller"]?.ToString();
            var action = ViewContext.RouteData.Values["Action"]?.ToString();

            if (controller == null || action == null)
            {
                return View((IEnumerable<BreadcrumbViewModel>)null);
            }

            var breadcrumbs = _siteMapBuilder.Build(controller, action);

            return View(breadcrumbs);
        }
    }
}
