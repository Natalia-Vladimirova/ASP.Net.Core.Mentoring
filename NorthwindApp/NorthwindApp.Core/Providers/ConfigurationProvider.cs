using System;
using Microsoft.Extensions.Configuration;

namespace NorthwindApp.Core.Providers
{
    public class ConfigurationProvider : Interfaces.IConfigurationProvider
    {
        private const string ProductPageSizeKey = "MaxProductsCount";
        private const string CategoryImageGarbageSizeKey = "CategoryImageGarbageSize";
        private const string ImageCacheFolderPathKey = "ImageCacheFolderPath";
        private const string MaxCachedImagesCountKey = "MaxCachedImagesCount";
        private const string CacheExpirationTimeKey = "CacheExpirationTime";
        private const string LogActionMethodCallsKey = "LogActionMethodCalls";

        private readonly IConfiguration _configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual int ProductPageSize => GetInt(ProductPageSizeKey);

        public int CategoryImageGarbageSize => GetInt(CategoryImageGarbageSizeKey);

        public string ImageCacheFolderPath => _configuration[ImageCacheFolderPathKey];

        public int MaxCachedImagesCount => GetInt(MaxCachedImagesCountKey);

        public TimeSpan CacheExpirationTime => TimeSpan.Parse(_configuration[CacheExpirationTimeKey]);

        public bool LogActionMethodCalls => GetBool(LogActionMethodCallsKey);

        private int GetInt(string key) => int.TryParse(_configuration[key], out var result)
            ? result
            : 0;

        private bool GetBool(string key) => bool.TryParse(_configuration[key], out var result)
            ? result
            : false;
    }
}
