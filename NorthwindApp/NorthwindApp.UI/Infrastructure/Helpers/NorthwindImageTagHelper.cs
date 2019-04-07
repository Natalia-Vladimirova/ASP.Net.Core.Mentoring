using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace NorthwindApp.UI.Infrastructure.Helpers
{
    [HtmlTargetElement("a", Attributes = "northwind-id", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class NorthwindImageTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory _urlHelperFactory;

        public NorthwindImageTagHelper(IUrlHelperFactory helperFactory)
        {
            _urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public int NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

            output.Attributes.SetAttribute("href", urlHelper.Action("Image", "Category", new { id = NorthwindId }));
        }
    }
}
