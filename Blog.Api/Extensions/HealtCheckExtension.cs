using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;


namespace Blog.Api.Extensions
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddProjectHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var redisHost = configuration.GetValue<string>("RedisSettings:Host");
            var redisPort = configuration.GetValue<string>("RedisSettings:Port");
            var redisConnectionString = $"{redisHost}:{redisPort}";
            const int MaxRetries = 10;
            const int DelayInSeconds = 5;
            string rabbitMqUri = string.Empty;
            for (int i = 0; i < MaxRetries; i++)
            {
                try
                {
                    rabbitMqUri = configuration.GetConnectionString("RabbitMQ")
                            ?? throw new InvalidOperationException("RabbitMQ bağlantı dizesi yapılandırmada bulunamadı.");
                    Console.WriteLine("RabbitMQ bağlantısı başalı");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RabbitMQ bağlantısı başarısız. ({i + 1}/{MaxRetries}). {DelayInSeconds} saniye sonra tekrar denenecek. Hata: {ex.Message}");
                    if (i == MaxRetries - 1)
                    {
                        throw;
                    }
                    Thread.Sleep(TimeSpan.FromSeconds(DelayInSeconds));
                }
            }


            var postgreSqlConnection = configuration.GetConnectionString("BlogDB")
                                       ?? throw new InvalidOperationException("BlogDB bağlantı dizesi yapılandırmada bulunamadı.");

            services.AddHealthChecks()
                .AddRedis(
                    redisConnectionString: redisConnectionString,
                    name: "Redis Check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "ready", "redis" })

                .AddRabbitMQ(
                    factory: async serviceProvider =>
                    {
                        var factory = new ConnectionFactory()
                        {
                            Uri = new Uri(rabbitMqUri)
                        };
                        return await factory.CreateConnectionAsync();
                    },
                    name: "RabbitMQ Check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "ready", "rabbitMq" })

                .AddNpgSql(
                    connectionString: postgreSqlConnection,
                    name: "DataBase Check",
                    failureStatus: HealthStatus.Unhealthy,
                    timeout: TimeSpan.FromSeconds(5),
                    tags: new[] { "ready", "postgres" });

            return services;
        }
    }
}