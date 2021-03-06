﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
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

        private readonly int _cacheMinutes;
        private readonly IMemoryCache _memoryCache;

        public CasheProvider(IMemoryCache memoryCache, IConfiguration configuration)
        {
            _memoryCache = memoryCache;
            _cacheMinutes = int.Parse(configuration.GetSection("AppSettings:DefaultCacheMinutesTime").Value);
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
