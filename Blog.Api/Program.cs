using Asp.Versioning;
using Blog.Api.Extensions;
using Blog.Api.Middlewares;
using Blog.APi.Filters;
using Blog.APi.Middlewares;
using Blog.Business;
using Blog.Business.AutoMapper;
using Blog.Core.Utilities;
using Blog.Dto.Validators.Auth;
using Blog.Repository.EntityFramework.Context;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


#region App Configuration
configuration.GetSection("TokenOptions").Bind(TokenHelper.TokenSettings);
builder.Services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
builder.Services.Configure<MailOptions>(configuration.GetSection("EmailSettings"));
#endregion

#region Rate Limiter & Health Check
builder.Services.RateLimiter();
builder.Services.AddProjectHealthChecks(configuration);
#endregion

builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

#region Filters, Controllers ve Newtonsoft
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Formatting = Formatting.Indented;
});
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
    try
    {
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
            Console.WriteLine("Database migrations applied successfully.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while applying migrations: {ex.Message}");
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
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

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