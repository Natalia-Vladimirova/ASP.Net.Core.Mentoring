using Microsoft.Extensions.Configuration;

namespace NorthwindApp.Core.Providers
{
    public class ConfigurationProvider : Interfaces.IConfigurationProvider
    {
        private const string ProductPageSizeKey = "MaxProductsCount";
        private const string CategoryImageGarbageSizeKey = "CategoryImageGarbageSize";

        private readonly IConfiguration _configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual int ProductPageSize => GetInt(ProductPageSizeKey);

        public int CategoryImageGarbageSize => GetInt(CategoryImageGarbageSizeKey);

        private int GetInt(string key) => int.TryParse(_configuration[key], out var result)
            ? result
            : 0;
    }
}
