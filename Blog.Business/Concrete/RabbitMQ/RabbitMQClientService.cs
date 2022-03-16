using Blog.Business.Abstract.RabbitMQ;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Concrete.RabbitMQ
{
    public class RabbitMQClientService : IRabbitMQClientService
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "blog";

        public RabbitMQClientService(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public IModel Connect(string queName)
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            //direct kullandım cünkü route bilgisine göre göndericem alicilara(n11,trendyol vs routekeylerim)
            _channel.ExchangeDeclare(ExchangeName, type: ExchangeType.Direct, true, false);
            //kuyruk tanımlıyorum(kuyruk adı,rabitmqresetolursaverisilinmesin,farklı kanallardan baglanilsin, kuyruğa bagli olan son subcriberda baglanti kopartırsa kuyurk silinmesin)
            _channel.QueueDeclare(queName, true, false, false, null);
            _channel.QueueBind(exchange: ExchangeName, queue: queName, routingKey: "route-" + queName);
            Console.WriteLine("RabbitMQ Connection Success");
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();

            _connection?.Close();
            _connection?.Dispose();
            Console.WriteLine("RabbitMQ Connection Fail");

        }
    }
}
