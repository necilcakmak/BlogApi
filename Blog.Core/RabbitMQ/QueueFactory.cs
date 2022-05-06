using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace Blog.Core.RabbitMQ
{
    public class QueueFactory
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;

        public QueueFactory(ConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public void Publish<T>(T data, string queueName = RabbitMQConst.MailQueue)
        {
            var channel = Connect(queueName);
            var bodyString = JsonConvert.SerializeObject(data);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMQConst.ExchangeName, routingKey: queueName, false, basicProperties: properties, body: bodyByte);
        }
        public IModel Connect(string queueName = RabbitMQConst.MailQueue)
        {
            _connection = _connectionFactory.CreateConnection();
            if (_channel is { IsOpen: true })
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(RabbitMQConst.ExchangeName, type: ExchangeType.Direct, true, false);
            //kuyruk tanımlıyorum(kuyruk adı,rabitmqresetolursaverisilinmesin,farklı kanallardan baglanilsin, kuyruğa bagli olan son subcriberda baglanti kopartırsa kuyurk silinmesin)
            _channel.QueueDeclare(queueName, true, false, false, null);
            _channel.QueueBind(exchange: RabbitMQConst.ExchangeName, queue: queueName, routingKey: queueName);
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
