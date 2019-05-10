using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NorthwindApp.UI.Infrastructure.Configuration;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Services
{
    public class DirectoryCacheService : ICacheService
    {
        private readonly object _purgeLock = new object();

        private readonly FileCacheOptions _fileCacheOptions;
        private readonly string _basePath;

        public DirectoryCacheService(IOptions<FileCacheOptions> fileCacheOptions, string basePath)
        {
            _fileCacheOptions = fileCacheOptions.Value;
            _basePath = basePath;
        }

        public async Task<byte[]> GetAsync(string key)
        {
            if (!IsCached(key))
            {
                return null;
            }

            var filePath = GetFilePath(key);

            if (RemoveFileIfExpired(filePath, _fileCacheOptions.CacheExpirationTime))
            {
                return null;
            }

            var content = await File.ReadAllBytesAsync(filePath);
            UpdateCachedFileLastAccess(filePath);
            PurgeExcessMemoryAsync();

            return content;
        }

        public async Task AddAsync(string key, byte[] content)
        {
            if (IsAvailableToCache())
            {
                var filePath = GetFilePath(key);
                await File.WriteAllBytesAsync(filePath, content);
                UpdateCachedFileLastAccess(filePath);
            }

            PurgeExcessMemoryAsync();
        }

        private bool RemoveFileIfExpired(string filePath, TimeSpan cacheExpirationTime)
        {
            if (GetCachedFileLastAccess(filePath) + cacheExpirationTime < DateTime.UtcNow)
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

        private DateTime GetCachedFileLastAccess(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.LastAccessTimeUtc;
        }

        private void UpdateCachedFileLastAccess(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            fileInfo.LastAccessTimeUtc = DateTime.UtcNow;
        }

        private bool IsCached(string fileName)
        {
            return Directory.EnumerateFiles(GetCacheFolderPath())
                .FirstOrDefault(x => string.Equals(Path.GetFileName(x), fileName, StringComparison.OrdinalIgnoreCase)) != null;
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(GetCacheFolderPath(), fileName);
        }

        private string GetCacheFolderPath()
        {
            var cacheFolderPath = Path.Combine(_basePath, _fileCacheOptions.ImageCacheFolderPath);

            if (!Directory.Exists(cacheFolderPath))
            {
                Directory.CreateDirectory(cacheFolderPath);
            }

            return cacheFolderPath;
        }

        private string[] GetCacheFilesPaths() => Directory.GetFiles(GetCacheFolderPath());

        private bool IsAvailableToCache() => GetCacheFilesPaths().Length < _fileCacheOptions.MaxCachedImagesCount;

        private void PurgeExcessMemoryAsync()
        {
            Task.Run(() =>
            {
                if (IsAvailableToCache())
                {
                    return;
                }

                lock (_purgeLock)
                {
                    if (IsAvailableToCache())
                    {
                        return;
                    }

                    var cacheExpirationTime = _fileCacheOptions.CacheExpirationTime;

                    foreach(var filePath in GetCacheFilesPaths())
                    {
                        RemoveFileIfExpired(filePath, cacheExpirationTime);
                    }
                }
            });
        }
    }
}
