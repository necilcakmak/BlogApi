var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false)
    .AddJsonFile($"appsettings.{environmentName}.json", true)
    .AddEnvironmentVariables()
    .Build();
builder.Services
    .AddHealthChecksUI(settings =>
    {
        settings.AddHealthCheckEndpoint("Api Service", configuration.GetConnectionString("ApiService"));
        settings.SetEvaluationTimeInSeconds(5);
        settings.SetApiMaxActiveRequests(2);
    }).AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecksUI(options => { options.UIPath = "/health-ui"; });

app.Run();