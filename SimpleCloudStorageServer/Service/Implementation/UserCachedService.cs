using SimpleCloudStorageServer.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public class UserCachedService : IUserCachedService
    {

        private readonly ICacheProvider _cacheProvider;
        private readonly IUserRepository _userRepository;

        public UserCachedService(ICacheProvider cacheProvider, IUserRepository userRepository)
        {
            _cacheProvider = cacheProvider;
            _userRepository = userRepository;
        }

        private async Task<User> GetUser(string key, Func<Task<User>> getUser)
        {
            var user = _cacheProvider.GetFromCache<User>(key);

            if (user != null) return user;

            user = await getUser();

            if (user != null) _cacheProvider.SetCache(user.AppId, user);

            return user;
        }

        public void RemoveUserFromCache(string key)
        {
            _cacheProvider.ClearCache(key);
        }

        public void AddUser(User user, string key)
        {
            _cacheProvider.SetCache<User>(key, user);
        }

        public Task<User> GetUser(string key)
        {
            return GetUser(key, () => _userRepository.GetUserByAppId(key));
        }
    }
}
