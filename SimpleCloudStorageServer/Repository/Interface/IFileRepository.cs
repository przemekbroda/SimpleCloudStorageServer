using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public interface IFileRepository : IBaseRepository<File>
    {
        public Task<File> GetFileByUserAppIdAndFileName(string appId, string fileName);
        public Task<File> GetFileByUserIdAndFileName(long userId, string fileName);

    }
}
