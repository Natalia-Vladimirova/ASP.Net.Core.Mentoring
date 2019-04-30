using System;
using System.IO;
using System.Reflection;
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
        private readonly INorthwindApiClient _client;

        public CategoriesControllerTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _client = new NorthwindApiClient(new Uri("http://localhost:5000"));
        }

        [Fact]
        public async Task Test_GetCategories()
        {
            var categories = await _client.GetCategoriesAsync();
            _testOutputHelper.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));
        }

        [Fact]
        public async Task Test_UploadCategoryImage_GetCategoryImage()
        {
            var categoryId = 2;
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var image = await File.ReadAllBytesAsync(Path.Combine(folder, "checkmark-green.png"));

            using (var stream = new MemoryStream(image))
            {
                await _client.UploadCategoryImageAsync(categoryId, stream);
            }

            var uploadedImage = await _client.GetCategoryImageAsync(categoryId) as byte[];

            Assert.Equal(image, uploadedImage);
        }
    }
}
