using Blog.Business.Abstract.RabbitMQ;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Concrete.RabbitMQ
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {
        private readonly IRabbitMQClientService _rabbitMQClientService;
        public RabbitMQPublisher(IRabbitMQClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish<T>(T data)
        {
            var channel = _rabbitMQClientService.Connect("blogque");
            var bodyString = JsonConvert.SerializeObject(data);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQClientService.ExchangeName, routingKey: "route-blogque", false, basicProperties: properties, body: bodyByte);
        }
    }
}
