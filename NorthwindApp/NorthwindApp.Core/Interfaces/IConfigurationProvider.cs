using System;

namespace NorthwindApp.Core.Interfaces
{
    public interface IConfigurationProvider
    {
        int DefaultProductPageSize { get; }

        int CategoryImageGarbageSize { get; }

        string ImageCacheFolderPath { get; }

        int MaxCachedImagesCount { get; }

        TimeSpan CacheExpirationTime { get; }

        bool LogActionMethodCalls { get; }
    }
}
