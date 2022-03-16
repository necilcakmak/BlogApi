using Blog.WorkerService;
using Blog.WorkerService.Services.Abstract;
using Blog.WorkerService.Services.Concrete;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration Configuration = hostContext.Configuration;
        services.AddSingleton<IRabbitMQClientService, RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() 
        { 
            Uri = new Uri(Configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true 
        });
        services.AddHostedService<MailWorker>();
    })
    .Build();

await host.RunAsync();
