using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Utilities
{
    public class RedisSettings
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string RedisPassword { get; set; }
        public int RedisExpireTime { get; set; }
        public string EnvironmentName { get; set; }
    }
}
