using Blog.Business.Abstract;
using Blog.Business.Abstract.RabbitMQ;
using Blog.Business.Abstract.RedisCache;
using Blog.Business.Concrete;
using Blog.Business.Concrete.RabbitMQ;
using Blog.Business.Concrete.RedisCache;
using Blog.Core.Utilities;
using Blog.Core.Utilities.Abstract;
using Blog.Core.Utilities.Concrete;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Blog.Repository.EntityFramework.Concrete.UnitOfWork;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Blog.Business
{
    public static class ServiceCollections
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<BlogDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("BlogDB"), b => b.MigrationsAssembly("Blog.Api")));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IArticleService, ArticleService>();
            serviceCollection.AddScoped<ICategoryService, CategoryService>();
            serviceCollection.AddScoped<IParentCategoryService, ParentCategoryService>();
            serviceCollection.AddScoped<ICommentService, CommentService>();
            serviceCollection.AddTransient<IAuthService, AuthService>();
            serviceCollection.AddSingleton<IHashManager, HashManager>();
            serviceCollection.AddSingleton<IMailService, MailService>();

            serviceCollection.AddSingleton<IRedisService, RedisService>();
            serviceCollection.AddSingleton(sp => new ConnectionFactory() { Uri = new Uri(configuration.GetConnectionString("RabbitMQ")), DispatchConsumersAsync = true, });
            serviceCollection.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
            serviceCollection.AddScoped<IRabbitMQClientService, RabbitMQClientService>();
            return serviceCollection;
        }

    }
}
