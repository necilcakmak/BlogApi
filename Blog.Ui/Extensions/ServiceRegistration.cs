using Blog.Ui.Services;
using Blog.Ui.Services.Auth;

namespace Blog.Ui.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection UiServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
