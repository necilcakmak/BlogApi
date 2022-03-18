﻿using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Mapping;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.EntityFramework.Context
{
    public class BlogDbContext : DbContext
    {
        public static Guid UserId;
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSetting> UserSettings { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<UserFollowed> UserFolloweds { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new ArticleMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new MainCategoryMap());
            modelBuilder.ApplyConfiguration(new UserSettingsMap());
            modelBuilder.ApplyConfiguration(new FollowedAuthorsMap());
            modelBuilder.ApplyConfiguration(new FollowersAuthorsMap());
        }
    }
}
