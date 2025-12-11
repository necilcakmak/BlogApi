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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(false);

            builder.Property(c => c.Text).HasMaxLength(500);
            builder.Property(c => c.Text).IsRequired();

            builder.Property(c => c.UserId).IsRequired().HasDefaultValue(BlogDbContext.UserId);

            builder.HasOne(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);
            builder.HasOne(c => c.User).WithMany(a => a.Comments).HasForeignKey(c => c.UserId);

            builder.ToTable("Comments");
            builder.HasData(new Comment
            {
                Id = new Guid("c8d2fc77-9c77-48fe-9e7b-4f47c34fe27e"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                Text = "örnek yorum 1",
                ArticleId = new Guid("507462a3-5639-4573-b7d9-306d560a7ca8"),
                UserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),

            });
        }
    }
}
