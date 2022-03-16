using Blog.WorkerService.Services.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Blog.WorkerService
{
    public class MailWorker : BackgroundService
    {
        private readonly ILogger<MailWorker> _logger;
        private readonly IRabbitMQClientService _rabbitMQClientService;
        private IModel _channel;
        public MailWorker(ILogger<MailWorker> logger, IRabbitMQClientService rabbitMQClientService)
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
            _channel.BasicConsume("blogque", false, consumer);
            //mesaj geldiginde calissin
            consumer.Received += Consumer_Received;//received cagiralim (lamba yerine ayr� methodta temiz yazd�k)
            return Task.CompletedTask;//i�lemin bitti�ini d�nelim.
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            List<MailList> mailLists = JsonConvert.DeserializeObject<List<MailList>>(Encoding.UTF8.GetString(@event.Body.ToArray()));
            foreach (var item in mailLists)
            {
                _logger.LogInformation($"{item.Mail} mail g�nderildi...");
            }
            _channel.BasicAck(@event.DeliveryTag, false);
        }
    }
}