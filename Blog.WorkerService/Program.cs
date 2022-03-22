using Blog.WorkerService;
using Blog.WorkerService.Services.Abstract;
using Blog.WorkerService.Services.Concrete;
using RabbitMQ.Client;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var environmentName = hostContext.HostingEnvironment.EnvironmentName;

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddJsonFile($"appsettings.{environmentName}.json", true)
            .AddEnvironmentVariables()
            .Build();
        services.AddSingleton<IRabbitMQClientService, RabbitMQClientService>();
        services.AddSingleton(sp => new ConnectionFactory() 
        { 
            Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true 
        });
        services.AddHostedService<MailWorker>();
    })
    .Build();

await host.RunAsync();
