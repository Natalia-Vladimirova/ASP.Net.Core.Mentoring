using System.Collections.Generic;
using NorthwindApp.UI.Models;

namespace NorthwindApp.UI.Interfaces
{
    public interface ISiteMapBuilder
    {
        IEnumerable<BreadcrumbViewModel> Build(string controller, string action);
    }
}
