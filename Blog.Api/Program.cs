using Blog.APi.Filters;
using Blog.APi.Middlewares;
using Blog.Business;
using Blog.Business.AutoMapper;
using Blog.Core.Utilities;
using Blog.Dto.Validators.Auth;
using Blog.Repository.EntityFramework.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region filters, fluentvalidation ve newtonsoft
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ValidationFilter));
    options.EnableEndpointRouting = false;
}).AddFluentValidation(fv =>
{
    fv.RegisterValidatorsFromAssemblyContaining<RegisterValidator>();
}).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

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
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "V1",
        Title = "Blog Api",
        Description = "ASP.NET Core 6.0 Web API"
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                    }
                });
});
#endregion

#region token settings
var tokenOptions = builder.Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();
var key = Encoding.ASCII.GetBytes(tokenOptions.SecurityKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(key),
    };
});
#endregion

#region inject my services
string dbConnection = builder.Configuration.GetConnectionString("BlogDB");
builder.Services.LoadMyServices(dbConnection);
#endregion

#region add auto mapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));
#endregion

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

/////////////////////////////////////////////////////////////////////////////////////////////////

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "Blog Api UI";
    });
}

#region cors ayarlari
app.UseCors(options => options
.WithOrigins(new[] { "http://localhost:8080", "http://localhost:4000" })
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
);
#endregion

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<BlogDbContext>();
    context.Database.EnsureCreated();
}
app.Run();
