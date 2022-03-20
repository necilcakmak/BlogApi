using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Blog.Api.Extensions
{
    public static class HealtCheckExtension
    {
        public static IServiceCollection HealtCheck(this IServiceCollection service, IConfiguration configuration)
        {
            var redisEndPoint = configuration.GetValue<string>("RedisSettings:RedisEndPoint");
            var redisPort = configuration.GetValue<string>("RedisSettings:RedisPort");
            service.AddHealthChecks()
                    .AddRedis(
                    redisConnectionString: redisEndPoint + ":" + redisPort,
                    name: "Redis Check",
                    failureStatus: HealthStatus.Unhealthy | HealthStatus.Degraded,
                    tags: new string[] { "redis" })
                    .AddRabbitMQ(
                    rabbitConnectionString: configuration.GetConnectionString("RabbitMQ"),
                    name: "RabbitMQ Check",
                    failureStatus: HealthStatus.Unhealthy | HealthStatus.Degraded,
                    tags: new string[] { "rabbitMq" })
                     .AddNpgSql(
                    npgsqlConnectionString: configuration.GetConnectionString("BlogDB"),
                    name: "DataBase Check",
                    failureStatus: HealthStatus.Unhealthy | HealthStatus.Degraded,
                    tags: new string[] { "Postgres" });
            return service;
        }
    }
}
