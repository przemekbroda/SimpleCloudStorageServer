using SimpleCloudStorageServer.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public class UserCachedService : IUserCachedService
    {

        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
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

            try
            {
                await semaphore.WaitAsync();

                user = _cacheProvider.GetFromCache<User>(key);

                if (user != null)
                {
                    return user;
                }

                user = await getUser();

                _cacheProvider.SetCache<User>(user.AppId, user);
            }
            finally
            {
                semaphore.Release();
            }

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
