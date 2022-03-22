using Blog.Api.Extensions;
using Blog.APi.Filters;
using Blog.APi.Middlewares;
using Blog.Business;
using Blog.Business.AutoMapper;
using Blog.Core.Utilities;
using Blog.Dto.Validators.Auth;
using Blog.Repository.EntityFramework.Context;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false)
    .AddJsonFile($"appsettings.{environmentName}.json", true)
    .AddEnvironmentVariables()
    .Build();
Console.WriteLine("env name:" + environmentName);
#region app configuration
builder.Services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
builder.Services.Configure<MailOptions>(configuration.GetSection("EmailSettings"));
#endregion

#region healt check
builder.Services.HealtCheck(builder.Configuration);
#endregion

#region filters, fluentvalidation ve newtonsoft
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
    options.EnableEndpointRouting = false;
}).AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<RegisterValidator>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
#endregion

#region api versioning
builder.Services.AddApiVersioning(v =>
{
    v.DefaultApiVersion = new ApiVersion(1, 0);//default version
    v.AssumeDefaultVersionWhenUnspecified = true;
    v.ReportApiVersions = true;//notify client of versions 
    v.ApiVersionReader = new HeaderApiVersionReader("api-version");
});
builder.Services.AddEndpointsApiExplorer();
#endregion

#region swagger settings
builder.Services.CustomSwagger();
#endregion

builder.Services.AddAuthentication();

#region inject my services
builder.Services.LoadMyServices(configuration);
#endregion

#region add auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));
#endregion

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//////////////////////////////////  APP   ///////////////////////////////////////////////////////////////

var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Blog Api UI";
    });
}

#region cors settings
app.UseCors(options => options
.WithOrigins(new[] { "http://20.124.207.158", "http://localhost", "http://localhost:8080", "http://localhost:5000", "http://localhost:4000", "http://localhost:3000" })
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
);
#endregion

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
