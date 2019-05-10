using Microsoft.Extensions.Configuration;

namespace NorthwindApp.Core.Providers
{
    public class ConfigurationProvider : Interfaces.IConfigurationProvider
    {
        private const string DefaultProductPageSizeKey = "DefaultProductPageSize";
        private const string CategoryImageGarbageSizeKey = "CategoryImageGarbageSize";
        private const string LogActionMethodCallsKey = "LogActionMethodCalls";

        private readonly IConfiguration _configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int DefaultProductPageSize => GetInt(DefaultProductPageSizeKey);

        public int CategoryImageGarbageSize => GetInt(CategoryImageGarbageSizeKey);

        public bool LogActionMethodCalls => GetBool(LogActionMethodCallsKey);

        private int GetInt(string key) => int.TryParse(_configuration[key], out var result)
            ? result
            : 0;

        private bool GetBool(string key) => bool.TryParse(_configuration[key], out var result)
            ? result
            : false;
    }
}
