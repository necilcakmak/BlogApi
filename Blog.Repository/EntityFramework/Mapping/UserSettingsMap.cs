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
    class UserSettingsMap : IEntityTypeConfiguration<UserSetting>
    {
        public void Configure(EntityTypeBuilder<UserSetting> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(a => a.ReceiveMail).IsRequired().HasDefaultValue(true);

            builder.Property(a => a.IsApproved).IsRequired().HasDefaultValue(false);

            builder.Property(a => a.NewBlog).IsRequired().HasDefaultValue(true);

            builder.HasOne(a => a.User).WithOne(c => c.UserSetting).HasForeignKey<UserSetting>(a => a.UserId);

            builder.ToTable("UserSettings");
            builder.HasData(
                new UserSetting
                {
                    Id = new Guid("8ff845a2-ad00-4158-8b4a-061a5764c789"),
                    IsActive = true,
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                    ReceiveMail = true,
                    NewBlog = true,
                    IsApproved = true,
                    RoleValue = 15
                },
                new UserSetting
                {
                    Id = new Guid("e88c8860-4b9a-4736-9ed3-5cf23a75a86b"),
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),
                    IsActive = true,
                    UserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                    ReceiveMail = true,
                    NewBlog = true,
                    IsApproved = false,
                    RoleValue = 11
                },
                new UserSetting
                {
                    Id = new Guid("a6bacb41-6666-4f2c-b5c8-afdac80f2026"),
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),
                    IsActive = true,
                    UserId = new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8"),
                    ReceiveMail = true,
                    NewBlog = true,
                    IsApproved = true,
                    RoleValue = 1
                });
        }
    }
}
