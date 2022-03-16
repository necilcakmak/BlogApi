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
    class FollowersAuthorsMap : IEntityTypeConfiguration<FollowersAuthors>
    {
        public void Configure(EntityTypeBuilder<FollowersAuthors> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.FollowedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasOne(a => a.UserSetting).WithMany(c => c.FollowersAuthors).HasForeignKey(a => a.UserSettingId);

            builder.ToTable("FollowersAuthors");
            builder.HasData(
                new FollowersAuthors
                {
                    Id = new Guid("a07a6456-3576-48ad-8914-1cd5c1152904"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                    UserSettingId = new Guid("8ff845a2-ad00-4158-8b4a-061a5764c789"),
                });
        }
    }
}
