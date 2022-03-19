using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Caching
{
    public class RedisNotAvailableException : Exception
    {
        public override string Message
        {
            get
            {
                return "Redis Server is not available.";
            }
        }
    }
}
