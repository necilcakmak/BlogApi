using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Blog.Api.Extensions
{
    public static class HealtCheckConfigureExtension
    {
        public static IApplicationBuilder UseCustomHealtCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/api/healt", new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    await context.Response.WriteAsync("OK");
                }
            });
            return app;
        }
    }
}
