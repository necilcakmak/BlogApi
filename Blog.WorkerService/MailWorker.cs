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
        private IModel _channel;
        public MailWorker(ILogger<MailWorker> logger, QueueFactory rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMQClientService.Connect();
            //mesajlar� kac kac alicam(her hangi bir boyut olabilir, 1 rer tane mesaj gelsin, global olsun mu(her bir dinleyiciye 5 er 5er gider,true ise tek seferde 5/subscribe gibi gider))
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);//channel verelim
            //ilgili kuyru�u dinle,mesaj geldiginde silme ben haber vericem(false)
            _channel.BasicConsume(RabbitMQConst.MailQueue, false, consumer);
            //mesaj geldiginde calissin
            consumer.Received += Consumer_Received;//received cagiralim (lamba yerine ayr� methodta temiz yazd�k)
            return Task.CompletedTask;//i�lemin bitti�ini d�nelim.
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            MailConfirmation user = JsonConvert.DeserializeObject<MailConfirmation>(Encoding.UTF8.GetString(@event.Body.ToArray()));

            //mail service burada post aticak
            _logger.LogInformation($"{user.FirstName} {user.LastName} adli kullanicinin {user.Email} adresine mail g�nderildi...");

            _channel.BasicAck(@event.DeliveryTag, false);
        }
    }
}