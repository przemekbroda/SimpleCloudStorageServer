using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public interface IUserCachedService
    {
        public Task<User> GetUser(string key);
        public void RemoveUserFromCache(string key);
        public void AddUser(User user, string key);
    }
}
