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

            builder.Property(c => c.Slug).HasMaxLength(150);
            builder.Property(c => c.Slug).IsRequired();

            builder.Property(c => c.Keywords).HasMaxLength(250);

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
                Content = "ilk makalenin içeriği",
                Thumbnail = "blog.jpg",
                LikeCount = 33,
                CommentCount = 2,
                Slug="ilk-makale",
                Keywords = "{}"
            });

        }
    }
}
