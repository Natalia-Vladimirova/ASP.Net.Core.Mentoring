using X.PagedList.Mvc.Common;

namespace NorthwindApp.UI.Infrastructure.Extensions
{
    public static class PagedOptionsExtensions
    {
        public static PagedListRenderOptionsBase GetCustomPagedListRenderOptions()
        {
            return new PagedListRenderOptionsBase
            {
                DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded,
                DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded,
                DisplayLinkToPreviousPage = PagedListDisplayMode.IfNeeded,
                DisplayLinkToNextPage = PagedListDisplayMode.IfNeeded,
                MaximumPageNumbersToDisplay = 5,
                DisplayEllipsesWhenNotShowingAllPageNumbers = true
            };
        }
    }
}
