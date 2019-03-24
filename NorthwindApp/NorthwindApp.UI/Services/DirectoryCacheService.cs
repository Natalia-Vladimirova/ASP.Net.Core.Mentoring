using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NorthwindApp.Core.Interfaces;
using NorthwindApp.UI.Interfaces;

namespace NorthwindApp.UI.Services
{
    public class DirectoryCacheService : ICacheService
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly string _basePath;

        private string GetCacheFolderPath()
        {
            var cacheFolderPath = Path.Combine(_basePath, _configurationProvider.ImageCacheFolderPath);

            if (!Directory.Exists(cacheFolderPath))
            {
                Directory.CreateDirectory(cacheFolderPath);
            }

            return cacheFolderPath;
        }

        public DirectoryCacheService(IConfigurationProvider configurationProvider, string basePath)
        {
            _configurationProvider = configurationProvider;
            _basePath = basePath;
        }

        public bool IsCached(string fileName)
        {
            return Directory.EnumerateFiles(GetCacheFolderPath())
                .FirstOrDefault(x => string.Equals(Path.GetFileName(x), fileName, StringComparison.OrdinalIgnoreCase)) != null;
        }

        public bool CanBeCached()
        {
            return Directory.GetFiles(GetCacheFolderPath()).Length < _configurationProvider.MaxCachedImagesCount;
        }

        public async Task<byte[]> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(GetCacheFolderPath(), fileName);
            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task AddFileAsync(string fileName, byte[] content)
        {
            var filePath = Path.Combine(GetCacheFolderPath(), fileName);
            await File.WriteAllBytesAsync(filePath, content);
        }

        public void RemoveFile(string fileName)
        {
            var filePath = Path.Combine(GetCacheFolderPath(), fileName);
            File.Delete(filePath);
        }
    }
}
