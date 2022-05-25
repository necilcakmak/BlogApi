using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blog.Repository.EntityFramework.Context
{
    public class BlogDbContext : DbContext
    {
        public static Guid UserId;
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ParentCategory> ParentCategories { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            //modelBuilder.ApplyConfiguration(new ArticleMap());
            //modelBuilder.ApplyConfiguration(new CategoryMap());
            //modelBuilder.ApplyConfiguration(new UserMap());
            //modelBuilder.ApplyConfiguration(new CommentMap());
            //modelBuilder.ApplyConfiguration(new ParentCategoryMap());
            //modelBuilder.ApplyConfiguration(new UserSettingMap());
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
