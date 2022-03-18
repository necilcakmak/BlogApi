using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract.RedisCache
{
    public interface IRedisService
    {
        Task<T?> GetAsync<T>(string key);
        Task<bool> InCache(string key);
        Task SetAsync(string key, object data);
        Task RemoveAsync(string key);
    }
}
