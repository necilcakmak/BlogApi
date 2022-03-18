using StackExchange.Redis;

namespace Blog.Api.Extensions
{
    public static class RedisConfigureExtension
    {
        public static IServiceCollection RedisSettings(this IServiceCollection service, IConfiguration Configuration)
        {
            service.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = Configuration.GetConnectionString("RedisCon");
                option.InstanceName = "test";
            });
            return service;
        }
    }
}
