using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Blog.Core.RabbitMQ;

public class QueueFactory : IQueueFactory
{
    private readonly ConnectionFactory _factory;
    private const string DefaultQueueName = "MailQueue";
    private const string ExchangeName = "DirectExchange";
    public QueueFactory(ConnectionFactory factory)
    {
        _factory = factory;
    }
    public async Task<IChannel> ConnectAsync(string queueName)
    {

        var connection = await _factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct, durable: true);
        await channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
        await channel.QueueBindAsync(queueName, ExchangeName, queueName);

        return channel;
    }
    public async Task PublishAsync<T>(T data, string queueName = DefaultQueueName)
    {
        var channel = await ConnectAsync(queueName);

        var bodyString = JsonConvert.SerializeObject(data);
        var bodyByte = Encoding.UTF8.GetBytes(bodyString);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        await channel.BasicPublishAsync(
            exchange: ExchangeName,
            routingKey: queueName,
            mandatory: false,
            basicProperties: properties,
            body: bodyByte);
    }

}
