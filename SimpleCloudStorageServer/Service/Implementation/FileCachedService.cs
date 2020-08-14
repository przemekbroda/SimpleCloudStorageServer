using SimpleCloudStorageServer.Model;
using SimpleCloudStorageServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service.Implementation
{
    public class FileCachedService : IFileCachedService
    {

        private readonly ICacheProvider _cacheProvider;
        private readonly IFileRepository _fileRepository;

        public FileCachedService(ICacheProvider cacheProvider, IFileRepository fileRepository)
        {
            _cacheProvider = cacheProvider;
            _fileRepository = fileRepository;
        }

        public Task<File> GetFile(string appId, string fileName)
        {
            string idFileName = prepareFileKey(appId, fileName);
            return GetFile(idFileName, () => _fileRepository.GetFileByUserAppIdAndFileName(appId, fileName));
        }

        public void RemoveFile(string appId, string fileName)
        {
            string idFileName = prepareFileKey(appId, fileName);
            _cacheProvider.ClearCache(idFileName);
        }

        public void SetFile(string appId, string fileName, File file)
        {
            string idFileName = prepareFileKey(appId, fileName);
            _cacheProvider.SetCache(idFileName, file);
        }

        private async Task<File> GetFile(string key, Func<Task<File>> GetFile)
        {
            var file = _cacheProvider.GetFromCache<File>(key);

            if (file != null) return file;

            file = await GetFile();

            if (file != null) _cacheProvider.SetCache(key, file);

            return file;
        }

        private string prepareFileKey(string appId, string fileName) => $"{appId}:{fileName}";
    }
}
