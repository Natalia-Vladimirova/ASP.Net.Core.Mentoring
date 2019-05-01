using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NorthwindApp.ConsoleApp.Services;

namespace NorthwindApp.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        private static async Task RunAsync()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            var northwindApiClient = new NorthwindApiClient(configuration);

            Console.WriteLine("Products");
            var products = await northwindApiClient.GetProductsAsync(1, 5);
            Console.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            Console.WriteLine("Categories");
            var categories = await northwindApiClient.GetCategoriesAsync();
            Console.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));
        }
    }
}
