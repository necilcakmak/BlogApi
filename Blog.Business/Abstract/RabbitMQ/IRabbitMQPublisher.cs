using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract.RabbitMQ
{
    public interface IRabbitMQPublisher
    {
        void Publish<T>(T data);
    }
}
