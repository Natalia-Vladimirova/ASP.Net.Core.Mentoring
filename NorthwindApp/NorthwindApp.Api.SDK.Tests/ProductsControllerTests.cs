using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NorthwindApp.Api.SDK.v1;
using Xunit;
using Xunit.Abstractions;

namespace NorthwindApp.Api.SDK.Tests
{
    public class ProductsControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly INorthwindAPI _client;

        public ProductsControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = new NorthwindAPI(new Uri("http://localhost:5000"));
        }

        [Fact]
        public async Task Test_GetProducts()
        {
            var products = await _client.GetProductsAsync(1, 3);
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));
        }

        [Fact]
        public async Task Test_GetProduct()
        {
            var product = await _client.GetProductAsync(4);
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(product, Formatting.Indented));
        }
    }
}
