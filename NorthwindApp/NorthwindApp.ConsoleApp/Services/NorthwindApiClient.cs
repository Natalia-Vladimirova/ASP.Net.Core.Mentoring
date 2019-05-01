using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest;
using Newtonsoft.Json;
using NorthwindApp.Models;

namespace NorthwindApp.ConsoleApp.Services
{
    public class NorthwindApiClient
    {
        private const string NorthwindApiUriKey = "NorthwindApiUri";

        private const string CategoriesUrl = "api/Categories";
        private const string ProductsUrl = "api/Products";

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public NorthwindApiClient(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize)
        {
            var queryParameters = new Dictionary<string, string>
            {
                { "page", page.ToString() },
                { "pageSize", pageSize.ToString() }
            };
            var uri = BuildUri(ProductsUrl, queryParameters);

            return await GetList<Product>(uri);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var uri = BuildUri(CategoriesUrl, null);

            return await GetList<Category>(uri);
        }

        private async Task<IEnumerable<T>> GetList<T>(Uri uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            var response = await _httpClient.SendAsync(request);
            var content = await response.Content?.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpOperationException
                {
                    Request = new HttpRequestMessageWrapper(request, null),
                    Response = new HttpResponseMessageWrapper(response, content)
                };
            }

            return JsonConvert.DeserializeObject<IEnumerable<T>>(content);
        }

        private Uri BuildUri(string endpoint, Dictionary<string, string> queryParameters)
        {
            var baseUri = new Uri(_configuration[NorthwindApiUriKey]);

            var endpointQuery = queryParameters == null
                ? endpoint
                : QueryHelpers.AddQueryString(endpoint, queryParameters);

            return new Uri(baseUri, endpointQuery);
        }
    }
}
