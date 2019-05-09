using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NorthwindApp.UI.Infrastructure.Extensions;
using NorthwindApp.UI.Services;

namespace NorthwindApp.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    scope.ServiceProvider
                        .GetRequiredService<RoleService>()
                        .InitializeRolesAsync()
                        .GetAwaiter()
                        .GetResult();
                }
                catch { }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseApplicationInsights()
                .UseSiteMap()
                .UseStartup<Startup>()
                .Build();
    }
}
