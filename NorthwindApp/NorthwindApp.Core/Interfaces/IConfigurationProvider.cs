using System;

namespace NorthwindApp.Core.Interfaces
{
    public interface IConfigurationProvider
    {
        int ProductPageSize { get; }

        int CategoryImageGarbageSize { get; }

        string ImageCacheFolderPath { get; }

        int MaxCachedImagesCount { get; }

        TimeSpan CacheExpirationTime { get; }

        bool LogActionMethodCalls { get; }
    }
}
