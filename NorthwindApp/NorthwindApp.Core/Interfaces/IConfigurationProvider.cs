namespace NorthwindApp.Core.Interfaces
{
    public interface IConfigurationProvider
    {
        int ProductPageSize { get; }

        int CategoryImageGarbageSize { get; }

        string ImageCacheFolderPath { get; }

        int MaxCachedImagesCount { get; }

        bool LogActionMethodCalls { get; }
    }
}
