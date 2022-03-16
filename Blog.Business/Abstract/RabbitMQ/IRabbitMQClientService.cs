using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract.RabbitMQ
{
    public interface IRabbitMQClientService
    {
        IModel Connect(string queName);
        void Dispose();
    }
}
