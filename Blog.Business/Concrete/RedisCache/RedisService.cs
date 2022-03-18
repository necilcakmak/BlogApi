using Blog.Business.Abstract.RedisCache;
using Blog.Core.Utilities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Blog.Business.Concrete.RedisCache
{
    public class RedisService : IRedisService
    {
        IDistributedCache _distributedCache;
        public RedisService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(jsonData))
            {

                return JsonConvert.DeserializeObject<T>(jsonData);
            }
            return default;

        }

        public async Task<bool> InCache(string key)
        {
            var jsonData = await _distributedCache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(jsonData))
            {
                return true;
            }
            return false;
        }

        public async Task SetAsync(string key, object data)
        {
            var jsonData = ExtensionFunctions.ToJson(data);
            var options = new DistributedCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromDays(1))
                        .SetAbsoluteExpiration(DateTime.Now.AddMonths(1));
            await _distributedCache.SetStringAsync(key, jsonData, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }
    }
}
