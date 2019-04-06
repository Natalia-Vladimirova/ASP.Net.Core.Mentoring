namespace NorthwindApp.UI.Models
{
    public class BreadcrumbViewModel
    {
        public string Controller { get; set; }

        public string Action { get; set; }

        public string Title { get; set; }

        public bool IsCurrentPage { get; set; }
    }
}
