using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace NorthwindApp.UI.Infrastructure.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseSiteMap(this IWebHostBuilder builder)
        {
            return builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                config
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("sitemap.json", false, true);
            });
        }
    }
}
