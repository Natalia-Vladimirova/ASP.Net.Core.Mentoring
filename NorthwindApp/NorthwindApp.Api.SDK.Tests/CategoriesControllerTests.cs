using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NorthwindApp.Api.SDK.v1;
using Xunit;
using Xunit.Abstractions;

namespace NorthwindApp.Api.SDK.Tests
{
    public class CategoriesControllerTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly INorthwindAPI _client;

        public CategoriesControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = new NorthwindAPI(new Uri("http://localhost:5000"));
        }

        [Fact]
        public async Task Test_GetCategories()
        {
            var categories = await _client.GetCategoriesAsync();
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));
        }
    }
}
