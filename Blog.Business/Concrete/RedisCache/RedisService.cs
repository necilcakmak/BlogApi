using Microsoft.Extensions.Options;
using ServiceStack.Redis;
using System.Text;
using Newtonsoft.Json;
using Blog.Business.Abstract.RedisCache;
using Blog.Core.Caching;
using Blog.Core.Utilities;

namespace Blog.Business.Concrete.RedisCache
{
    public class RedisService : IRedisService
    {
        public readonly RedisSettings _config;
        private readonly RedisEndpoint conf = null;

        public RedisService(IOptions<RedisSettings> config)
        {
            _config = config.Value;
            conf = new RedisEndpoint { Host = _config.Host, Port = Convert.ToInt32(_config.Port) };
            
        }
        public T Get<T>(string key)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                return client.Get<T>(key);
            }
            catch
            {
                throw new RedisNotAvailableException();
            }
        }

        public IList<T> GetAll<T>(string key)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                var keys = client.SearchKeys(key);
                if (keys.Any())
                {
                    IEnumerable<T> dataList = client.GetAll<T>(keys).Values;
                    return dataList.ToList();
                }
                return new List<T>();
            }
            catch
            {

                throw new RedisNotAvailableException();
            }
        }

        public void Set(string key, object data)
        {
            Set(key, data, DateTime.Now.AddMinutes(_config.RedisExpireTime));
        }

        public void Set(string key, object data, DateTime time)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                var dataSerialize = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.Objects
                });
                client.Set(key, Encoding.UTF8.GetBytes(dataSerialize), time);
            }
            catch
            {
                throw new RedisNotAvailableException();
            }
        }

        public void SetAll<T>(IDictionary<string, T> values)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                client.SetAll(values);
            }
            catch
            {

                throw new RedisNotAvailableException();
            }

        }

        public bool IsSet(string key)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                return client.ContainsKey(key);
            }
            catch
            {

                throw new RedisNotAvailableException();
            }
        }

        public void Remove(string key)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                client.Remove(key);
            }
            catch
            {
                throw new RedisNotAvailableException();
            }
        }

        public void RemoveByPattern(string pattern)
        {
            try
            {
                using IRedisClient client = new RedisClient(conf);
                var keys = client.SearchKeys(pattern);
                client.RemoveAll(keys);
            }
            catch
            {

                throw new RedisNotAvailableException();
            }
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public static string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}
