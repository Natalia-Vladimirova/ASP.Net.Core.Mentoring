namespace NorthwindApp.Core.Interfaces
{
    public interface IConfigurationProvider
    {
        int DefaultProductPageSize { get; }

        int CategoryImageGarbageSize { get; }

        bool LogActionMethodCalls { get; }
    }
}
