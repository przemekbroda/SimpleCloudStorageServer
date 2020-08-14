using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service.Implementation
{
    public interface IFileCachedService
    {
        public Task<File> GetFile(string appId, string fileName);
        public void SetFile(string appId, string fileName, File file);
        public void RemoveFile(string appId, string fileName);
    }
}
