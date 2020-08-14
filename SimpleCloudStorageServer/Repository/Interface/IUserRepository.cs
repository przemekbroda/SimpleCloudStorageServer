using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<User> GetUserByUsername(string username);
        public Task<User> GetUserByMainDirectory(string userDirectory);
        public Task<User> GetUserByAppId(string appId);
    }
}
