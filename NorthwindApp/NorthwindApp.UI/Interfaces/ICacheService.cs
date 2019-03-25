using System.Threading.Tasks;

namespace NorthwindApp.UI.Interfaces
{
    public interface ICacheService
    {
        Task<byte[]> GetAsync(string key);

        Task AddAsync(string key, byte[] content);
    }
}
