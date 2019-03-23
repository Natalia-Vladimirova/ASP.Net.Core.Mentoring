namespace NorthwindApp.Core.Interfaces
{
    public interface IConfigurationProvider
    {
        int ProductPageSize { get; }

        int CategoryImageGarbageSize { get; }
    }
}
