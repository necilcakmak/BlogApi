using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Mapping
{
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(c => c.UserId).IsRequired().HasDefaultValue(BlogDbContext.UserId);

            builder.Property(c => c.CategoryId).IsRequired();

            builder.Property(c => c.Title).HasMaxLength(50);
            builder.Property(c => c.Title).IsRequired();

            builder.Property(c => c.Description).HasMaxLength(150);
            builder.Property(c => c.Description).IsRequired();

            builder.Property(c => c.Content).HasMaxLength(10000);
            builder.Property(c => c.Content).IsRequired();

            builder.Property(c => c.Thumbnail).HasMaxLength(250).HasDefaultValue("default.jpg");


            builder.HasOne(a => a.User).WithMany(c => c.Articles).HasForeignKey(a => a.UserId);
            builder.HasOne(a => a.Category).WithMany(c => c.Articles).HasForeignKey(a => a.CategoryId);

            builder.ToTable("Articles");

            builder.HasData(new Article
            {
                Id = new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                UserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                CategoryId = new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"),
                PublishedDate = DateTime.Now.ToUniversalTime(),
                Title = "ilk makale",
                Description = "ilk makale açıklaması",
                Content = "ilk makalenin içeriği",
                Thumbnail = "default.jpg",
                ViewsCount = 33,
                CommentCount = 2,
            }, new Article
            {
                Id = new Guid("15cd7fe9-3d73-4028-b3f1-0e8a09112570"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                UserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                CategoryId = new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"),
                PublishedDate = DateTime.Now.ToUniversalTime(),
                Title = "ikinci makale",
                Description = "ikinci makale açıklaması",
                Content = "ikinci makalenin içeriği",
                Thumbnail = "default.jpg",
                ViewsCount = 25,
                CommentCount = 3,
            }, new Article
            {
                Id = new Guid("d1267b3b-c386-4481-804b-17c38c28d122"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                UserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                CategoryId = new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"),
                PublishedDate = DateTime.Now.ToUniversalTime(),
                Title = "üçüncü makale",
                Description = "üçüncü makale açıklaması",
                Content = "üçüncü makalenin içeriği",
                Thumbnail = "default.jpg",
                ViewsCount = 11,
                CommentCount = 1,
            }, new Article
            {
                Id = new Guid("ddb5c34f-518c-4189-ae3a-fe9103558500"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                UserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                CategoryId = new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"),
                PublishedDate = DateTime.Now.ToUniversalTime(),
                Title = "dördüncü makale",
                Description = "dördüncü makale açıklaması",
                Content = "dördüncü makalenin içeriği",
                Thumbnail = "default.jpg",
                ViewsCount = 10,
                CommentCount = 5,
            });

        }
    }
}
