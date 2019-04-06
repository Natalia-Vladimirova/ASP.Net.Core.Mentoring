using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NorthwindApp.UI.Infrastructure.Extensions;

namespace NorthwindApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseSiteMap()
                .UseStartup<Startup>()
                .Build();
    }
}
