using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Giphy.Api.Persistence
{
    public class CacheManager
    {
        private readonly IMemoryCache _cache;
        
        public CacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        internal async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> action)
        {
            T value;

            if(!_cache.TryGetValue<T>(key, out value))
            {
                value = await action();

                _cache.Set(key, value);
            }

            return value;
        }
    }
}