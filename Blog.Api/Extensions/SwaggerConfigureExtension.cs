using Microsoft.OpenApi;

namespace Blog.Api.Extensions
{
    public static class SwaggerConfigureExtension
    {
        public static IServiceCollection CustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Blog API",
                    Description = ".NET 10 Web API"
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Bearer authentication"
                };

                options.AddSecurityDefinition("Bearer", securityScheme);
                options.AddSecurityRequirement((document) => new OpenApiSecurityRequirement()
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = ["readAccess", "writeAccess"]
                });
            });

            return services;
        }
    }
}
