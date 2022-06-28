using Blog.Ui.Services;
using Blog.Ui.Services.Auth;
using Blog.Ui.Utils;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blog.Ui.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection UiServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<ModalManager>();
            services.AddScoped<IHttpClientService, HttpClientService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            return services;
        }
    }
}
