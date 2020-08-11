using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public interface ICacheProvider
    {
        T GetFromCache<T>(string key) where T : class;
        void SetCache<T>(string key, T value) where T : class;
        void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class;
        void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class;
        void ClearCache(string key);
    }
    public class CasheProvider : ICacheProvider
    {

        private const int _cacheMinutes = 10;

        private readonly IMemoryCache _memoryCache;

        public CasheProvider(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void ClearCache(string key)
        {
            _memoryCache.Remove(key);
        }

        public T GetFromCache<T>(string key) where T : class
        {
            return _memoryCache.Get<T>(key);
        }

        public void SetCache<T>(string key, T value) where T : class
        {
            _memoryCache.Set(key, value, DateTimeOffset.Now.AddMinutes(_cacheMinutes));
        }

        public void SetCache<T>(string key, T value, DateTimeOffset duration) where T : class
        {
            _memoryCache.Set(key, value, duration);
        }

        public void SetCache<T>(string key, T value, MemoryCacheEntryOptions options) where T : class
        {
            _memoryCache.Set(key, value, options);
        }
    }
}
