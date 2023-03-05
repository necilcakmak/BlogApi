using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Blog.Api.Middlewares
{
    public static class RateLimiterMiddleware
    {
        public static IServiceCollection RateLimiter(this IServiceCollection service)
        {
            service.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                 partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                 factory: partition => new FixedWindowRateLimiterOptions
                 {
                     AutoReplenishment = true,
                     PermitLimit = 20,
                     QueueLimit = 2,
                     QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                     Window = TimeSpan.FromMinutes(1)//1 dk da 5 requeste izin verdim(PermitLimit)
                 }));

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        await context.HttpContext.Response.WriteAsync(
                            $"To many request. Please try later {retryAfter.TotalMinutes} min. ", cancellationToken: token);
                    }
                    else
                    {
                        await context.HttpContext.Response.WriteAsync(
                            "To many request. Please try later. ", cancellationToken: token);
                    }
                };
                options.AddFixedWindowLimiter("Api", options =>
                {
                    options.AutoReplenishment = true;
                    options.PermitLimit = 5;
                    options.Window = TimeSpan.FromSeconds(10);
                });
            });
            return service;
        }
    }
}
