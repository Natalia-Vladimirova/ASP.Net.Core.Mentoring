using System;

namespace NorthwindApp.UI.Infrastructure.Configuration
{
    public class FileCacheOptions
    {
        public string ImageCacheFolderPath { get; set; }

        public int MaxCachedImagesCount { get; set; }

        public TimeSpan CacheExpirationTime { get; set; }
    }
}
