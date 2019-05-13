using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Services
{
    public class DirectoryCacheService : ICacheService
    {
        private readonly FileCacheOptions _fileCacheOptions;
        private readonly string _cacheFolderPath;
        private readonly IMemoryCache _memoryCache;

        public DirectoryCacheService(IOptions<FileCacheOptions> fileCacheOptions, string basePath)
        {
            _fileCacheOptions = fileCacheOptions.Value;
            _cacheFolderPath = Path.Combine(basePath, _fileCacheOptions.ImageCacheFolderPath);
            CreateCacheFolder();

            _memoryCache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = _fileCacheOptions.MaxCachedImagesCount
            });
        }

        public async Task<byte[]> GetAsync(string key)
        {
            if (!IsCached(key))
            {
                return null;
            }

            try
            {
                var filePath = GetFilePath(key);
                return await File.ReadAllBytesAsync(filePath);
            }
            catch
            {
                //catch any exception in order not to break execution if any error occurs when reading a file
                return null;
            }
        }

        public async Task AddAsync(string key, byte[] content)
        {
            try
            {
                var filePath = GetFilePath(key);
                await File.WriteAllBytesAsync(filePath, content);
            }
            catch
            {
                //catch any exception in order not to break execution if any error occurs when writing a file
                return;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetSlidingExpiration(_fileCacheOptions.CacheExpirationTime)
                .RegisterPostEvictionCallback(EvictionCallback);

            _memoryCache.Set(key, string.Empty, cacheEntryOptions);
        }

        private bool IsCached(string fileName)
        {
            return _memoryCache.TryGetValue(fileName, out string _);
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(_cacheFolderPath, fileName);
        }

        private void CreateCacheFolder()
        {
            if (!Directory.Exists(_cacheFolderPath))
            {
                Directory.CreateDirectory(_cacheFolderPath);
            }
        }

        private void EvictionCallback(object key, object value, EvictionReason reason, object state)
        {
            try
            {
                var filePath = GetFilePath((string) key);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch
            {
                //catch any exception in order not to break execution if any error occurs when removing a file
            }
        }
    }
}
