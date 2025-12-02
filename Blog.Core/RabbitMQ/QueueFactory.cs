using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

public class QueueFactory : IAsyncDisposable
{
    private readonly ConnectionFactory _connectionFactory;
    private IConnection? _connection;
    private IChannel? _channel;

    private const string DefaultQueueName = "MailQueue";
    private const string ExchangeName = "DirectExchange";

    public QueueFactory(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<IChannel> ConnectAsync(string queueName)
    {
        if (_channel is { IsOpen: true })
            return _channel;

        if (_connection is null || !_connection.IsOpen)
            _connection = await _connectionFactory.CreateConnectionAsync();

        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct, durable: true, autoDelete: false);
        await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
        await _channel.QueueBindAsync(ExchangeName, queueName, routingKey: queueName);

        Console.WriteLine("RabbitMQ Connection Success");

        return _channel;
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

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
        {
            if (_channel.IsOpen)
            {
                if (_channel.GetType().GetMethod("CloseAsync") is not null)
                    await _channel.CloseAsync();
            }

            _channel.Dispose();
            _channel = null;
        }

        if (_connection is not null)
        {
            if (_connection.IsOpen)
            {
                if (_connection.GetType().GetMethod("CloseAsync") is not null)
                    await _connection.CloseAsync();
                else
                    _connection.Dispose();
            }

            _connection.Dispose();
            _connection = null;
        }

        Console.WriteLine("RabbitMQ Disposed Successfully");

        GC.SuppressFinalize(this);
    }
}
