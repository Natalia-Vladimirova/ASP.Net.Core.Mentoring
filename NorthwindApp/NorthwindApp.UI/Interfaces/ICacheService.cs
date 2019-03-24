using System.Threading.Tasks;

namespace NorthwindApp.UI.Interfaces
{
    public interface ICacheService
    {
        bool IsCached(string fileName);

        bool CanBeCached();

        Task<byte[]> GetFileAsync(string fileName);

        Task AddFileAsync(string fileName, byte[] content);

        void RemoveFile(string fileName);
    }
}
