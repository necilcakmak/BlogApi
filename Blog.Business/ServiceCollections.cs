using Blog.Business.Abstract;
using Blog.Business.Concrete;
using Blog.Core.Utilities;
using Blog.Core.Utilities.Abstract;
using Blog.Core.Utilities.Concrete;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Blog.Repository.EntityFramework.Concrete.UnitOfWork;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Business
{
    public static class ServiceCollections
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<BlogDbContext>(options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("Blog.Api")));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddScoped<IArticleService, ArticleService>();
            serviceCollection.AddScoped<ICategoryService, CategoryService>();
            serviceCollection.AddScoped<IMainCategoryService, MainCategoryService>();
            serviceCollection.AddScoped<ICommentService, CommentService>();
            serviceCollection.AddTransient<IAuthService, AuthService>();
            serviceCollection.AddTransient<ITokenHelper, TokenHelper>();
            serviceCollection.AddSingleton<IHashManager, HashManager>();
            serviceCollection.AddSingleton<IMailService, MailService>();
            return serviceCollection;
        }

    }
}
