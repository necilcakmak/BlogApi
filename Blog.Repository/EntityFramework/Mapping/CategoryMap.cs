using Blog.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(c => c.MainCategoryId).IsRequired();

            builder.Property(c => c.CategoryName).HasMaxLength(50);
            builder.Property(c => c.CategoryName).IsRequired();
            builder.HasIndex(c => c.CategoryName).IsUnique();

            builder.Property(c => c.Description).HasMaxLength(100);
            builder.Property(c => c.Description).IsRequired();

            builder.HasOne(a => a.MainCategory).WithMany(c => c.Categories).HasForeignKey(a => a.MainCategoryId);

            builder.ToTable("Categories");
            builder.HasData(new Category
            {
                Id = new Guid("5533e9a6-186f-4a3d-9ef4-63a2f7c02eb2"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                MainCategoryId = new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"),
                CategoryName = "Yazılım",
                Description = "Yazılım kategorisine ait makaleler",
                TagName = "YZL"
            }, new Category
            {
                Id = new Guid("1bbc4e68-3e73-4f11-bd09-11ba71b5b582"),    
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                MainCategoryId = new Guid("11070708-1c30-4967-9bcf-433e703f348a"),
                CategoryName = "Sinema",
                Description = "Sinema kategorisine ait makaleler",
                TagName = "SİN"
            }); ;
        }
    }
}
