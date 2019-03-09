using Microsoft.Extensions.Configuration;

namespace NorthwindApp.Core.Providers
{
    public class ConfigurationProvider : Interfaces.IConfigurationProvider
    {
        private const string ProductPageSizeKey = "MaxProductsCount";

        private readonly IConfiguration _configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual int ProductPageSize => int.TryParse(_configuration[ProductPageSizeKey], out var result)
            ? result
            : 0;
    }
}
