using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NorthwindApp.UI.Infrastructure.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent NorthwindImageLink(this IHtmlHelper html, int id, string linkText)
        {
            return html.NorthwindImageLink(id, linkText, null);
        }

        public static IHtmlContent NorthwindImageLink(this IHtmlHelper html, int id, string linkText, object htmlAttributes)
        {
            return html.ActionLink(linkText, "Image", "Category", new { id }, htmlAttributes);
        }
    }
}
