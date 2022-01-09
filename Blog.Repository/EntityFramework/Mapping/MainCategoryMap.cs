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
    public class MainCategoryMap : IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(c => c.MainCategoryName).HasMaxLength(50);
            builder.Property(c => c.MainCategoryName).IsRequired();
            builder.HasIndex(c => c.MainCategoryName).IsUnique();

            builder.ToTable("MainCategories");
            builder.HasData(new MainCategory
            {
                Id = new Guid("eec3877e-de06-47a5-9f29-764cebf7851d"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                MainCategoryName = "Bilim",
            }, new MainCategory
            {
                Id = new Guid("11070708-1c30-4967-9bcf-433e703f348a"),
                IsActive = true,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                UpdatedDate = DateTime.Now.ToUniversalTime(),
                MainCategoryName = "Kültür"     
            });
        }
    }
}
