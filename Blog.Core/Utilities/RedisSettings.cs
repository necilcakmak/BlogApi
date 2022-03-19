using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Utilities
{
    public class RedisSettings
    {
        public string RedisEndPoint { get; set; }
        public string RedisPort { get; set; }
        public string RedisPassword { get; set; }
        public int RedisExpireTime { get; set; }
        public string EnvironmentName { get; set; }
    }
}
