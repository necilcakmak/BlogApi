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

            var rabbitMqUri = configuration.GetConnectionString("RabbitMQ")
                              ?? throw new InvalidOperationException("RabbitMQ bağlantı dizesi yapılandırmada bulunamadı.");

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