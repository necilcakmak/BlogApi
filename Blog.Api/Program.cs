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
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Blog.Core.Utilities.TokenHelper;


var builder = WebApplication.CreateBuilder(args);
var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false)
    .AddJsonFile($"appsettings.{environmentName}.json", true)
    .AddEnvironmentVariables()
    .Build();
#region app configuration
configuration.GetSection("TokenOptions").Bind(TokenHelper.TokenSettings);
builder.Services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
builder.Services.Configure<MailOptions>(configuration.GetSection("EmailSettings"));
#endregion

#region rate limiter
builder.Services.RateLimiter();
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
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();//api response value camelCase(ex:FirstName to firstName)
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    options.SerializerSettings.Formatting = Formatting.Indented;
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
#region rate limiter
app.UseRateLimiter();
#endregion

app.UseRouting();
#region cors settings
app.UseCors(options => options
.WithOrigins(new[] { "http://20.124.207.158", "http://localhost", "https://localhost:7064", "http://localhost:8080", "http://localhost:5000", "http://localhost:4000", "http://localhost:3000" })
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
app.UseAuthentication();
app.UseAuthorization();

//eger api ile ilgili bir istek gelirse api icin yazdigim middleware devreye girecek
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), appBuilder => appBuilder.UseMiddleware<AuthMiddleware>());
//route mekanizmasini belirtiyorum. Admin ve Api 2 ayri proje gibi calisacak
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
