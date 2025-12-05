using Asp.Versioning;
using Blog.Api.Extensions;
using Blog.Api.Middlewares;
using Blog.APi.Middlewares;
using Blog.Business;
using Blog.Business.AutoMapper;
using Blog.Core.Utilities;
using Blog.Repository.EntityFramework.Context;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{environmentName}.json", true)
    .AddEnvironmentVariables()
    .Build();


#region App Configuration
configuration.GetSection("TokenOptions").Bind(TokenHelper.TokenSettings);
builder.Services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
builder.Services.Configure<MailOptions>(configuration.GetSection("EmailSettings"));
#endregion
builder.Services.AddValidation();
#region Rate Limiter & Health Check
builder.Services.RateLimiter();
builder.Services.AddProjectHealthChecks(configuration);
#endregion

#region Filters, Controllers ve Newtonsoft
builder.Services.AddCustomControllers();
#endregion

#region API Versioning
builder.Services.AddApiVersioning(v =>
{
    v.DefaultApiVersion = new ApiVersion(1, 0);
    v.AssumeDefaultVersionWhenUnspecified = true;
    v.ReportApiVersions = true;
    v.ApiVersionReader = new HeaderApiVersionReader("api-version");
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

#region Swagger Settings
builder.Services.CustomSwagger();
#endregion

builder.Services.AddAuthentication();

#region Inject My Services
builder.Services.LoadMyServices(configuration);
#endregion

#region Add AutoMapper
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(MappingProfiles).Assembly);
});
#endregion

var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
    int retries = 5;
    int delay = 5000;

    while (retries > 0)
    {
        try
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
                Console.WriteLine("Database migrations applied successfully.");
            }
            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"Database migrations error: {ex.Message}. Retries left: {retries}");
            if (retries > 0)
            {
                Thread.Sleep(delay);
            }
            else
            {
                Console.WriteLine("All retries exhausted. Migration failed.");
                throw; // Son denemede hata fırlat
            }
        }
    }
}


app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.DocumentTitle = "Blog Api UI";
});

#region Rate Limiter
app.UseRateLimiter();
#endregion

app.UseRouting();

#region CORS Settings
app.UseCors(options => options
.WithOrigins("http://20.124.207.158", "http://localhost", "https://localhost:7064", "http://localhost:8080", "http://localhost:5000", "http://localhost:4000", "http://localhost:3000")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
);
#endregion

// Health Checks
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.UseStaticFiles();
//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder => appBuilder.UseMiddleware<AuthMiddleware>());

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "Admin",
        areaName: "Admin",
        pattern: "Admin/{controller=Home}/{action=Index}"
    );
    endpoints.MapControllerRoute(
        name: "Api",
        pattern: "Api/{controller=Home}/{action=Index}/{id?}"
    );
});


app.Run();