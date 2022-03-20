var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHealthChecksUI(settings =>
    {
        settings.AddHealthCheckEndpoint("Api Service", builder.Configuration.GetConnectionString("ApiService"));
        settings.SetEvaluationTimeInSeconds(5);
        settings.SetApiMaxActiveRequests(2);
    }).AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecksUI(options => { options.UIPath = "/health-ui"; });

app.Run();