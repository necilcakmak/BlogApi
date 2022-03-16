using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WorkerService.Services.Abstract
{
    public interface IRabbitMQClientService
    {
        IModel Connect();
        void Dispose();
    }
}
