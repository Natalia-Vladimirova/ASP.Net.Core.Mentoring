namespace NorthwindApp.UI.Infrastructure.Configuration
{
    public class Breadcrumb
    {
        public string Title { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public Breadcrumb[] Breadcrumbs { get; set; }
    }
}
