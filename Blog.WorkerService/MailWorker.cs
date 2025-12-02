using Blog.Core.RabbitMQ;
using Blog.Core.RabbitMQ.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Blog.WorkerService
{
    public class MailWorker : BackgroundService
    {
        private readonly ILogger<MailWorker> _logger;
        private readonly QueueFactory _rabbitMQClientService;
        private IChannel? _channel;

        public MailWorker(ILogger<MailWorker> logger, QueueFactory rabbitMQClientService)
        {
            _logger = logger;
            _rabbitMQClientService = rabbitMQClientService;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = await _rabbitMQClientService.ConnectAsync("MailQueue");
            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            await base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);

            _channel!.BasicConsumeAsync(
                queue: RabbitMQConst.MailQueue,
                autoAck: false,
                consumer: consumer);

            consumer.ReceivedAsync += Consumer_Received;

            return Task.CompletedTask;
        }


        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            var json = Encoding.UTF8.GetString(@event.Body.ToArray());
            var user = JsonConvert.DeserializeObject<MailConfirmation>(json);

            _logger.LogInformation(
                $"{user.FirstName} {user.LastName} adlı kullanıcının {user.Email} adresine mail gönderildi...");
            await _channel!.BasicAckAsync(@event.DeliveryTag, multiple: false);
        }
    }
}
