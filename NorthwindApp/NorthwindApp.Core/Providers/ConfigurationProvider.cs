using Microsoft.Extensions.Configuration;

namespace NorthwindApp.Core.Providers
{
    public class ConfigurationProvider : Interfaces.IConfigurationProvider
    {
        private const string ProductPageSizeKey = "MaxProductsCount";
        private const string CategoryImageGarbageSizeKey = "CategoryImageGarbageSize";
        private const string ImageCacheFolderPathKey = "ImageCacheFolderPath";
        private const string MaxCachedImagesCountKey = "MaxCachedImagesCount";

        private readonly IConfiguration _configuration;

        public ConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual int ProductPageSize => GetInt(ProductPageSizeKey);

        public int CategoryImageGarbageSize => GetInt(CategoryImageGarbageSizeKey);

        public string ImageCacheFolderPath => _configuration[ImageCacheFolderPathKey];

        public int MaxCachedImagesCount => GetInt(MaxCachedImagesCountKey);

        private int GetInt(string key) => int.TryParse(_configuration[key], out var result)
            ? result
            : 0;
    }
}
