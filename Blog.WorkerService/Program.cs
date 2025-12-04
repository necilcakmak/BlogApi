using Blog.Core.RabbitMQ;
using Blog.WorkerService;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;
        services.AddSingleton(sp =>
        {
            var connectionString = configuration.GetConnectionString("RabbitMQ")
                                   ?? throw new InvalidOperationException("RabbitMQ baðlantý dizesi bulunamadý.");

            return new ConnectionFactory()
            {
                Uri = new Uri(connectionString),
                ConsumerDispatchConcurrency = 5
            };
        });

        services.AddSingleton<QueueFactory>();
        services.AddHostedService<MailWorker>();
    })
    .Build();

await host.RunAsync();