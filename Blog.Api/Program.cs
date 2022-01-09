using Blog.APi.Filters;
using Blog.APi.Middlewares;
using Blog.Business;
using Blog.Business.AutoMapper;
using Blog.Core.Utilities;
using Blog.Dto.Validators.Auth;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
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

builder.Services.AddEndpointsApiExplorer();

#region swagger ayarlari
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
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
                            new string[] {}
                    }
                });
});
#endregion

#region token ayarlari
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

#region servisleri inject edelim
builder.Services.LoadMyServices(builder.Configuration.GetConnectionString("BlogDB"));
#endregion

#region auto mapper ekleyelim
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
    app.UseSwaggerUI();
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Images")),
    RequestPath = "/Images"
});
#region cors ayarlari
app.UseCors(options => options
.WithOrigins(new[] { "http://localhost:8080", "http://localhost:4000" })
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
);
#endregion

app.UseHttpsRedirection();
app.UseMiddleware<AuthMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
