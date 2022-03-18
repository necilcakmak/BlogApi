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
    class UserFollowersMap : IEntityTypeConfiguration<UserFollower>
    {
        public void Configure(EntityTypeBuilder<UserFollower> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.FollowersUserId).IsRequired().HasColumnType("uuid");

            builder.Property(c => c.FollowedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasOne(a => a.User).WithMany(c => c.UserFollowers).HasForeignKey(a => a.FollowersUserId);

            builder.ToTable("UserFollowers");
            builder.HasData(
                new UserFollower
                {
                    Id = new Guid("a07a6456-3576-48ad-8914-1cd5c1152904"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                    FollowersUserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                },
                new UserFollower
                {
                    Id = new Guid("1621633e-4c36-4a7e-a24c-aac0d35e4d9f"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                    FollowersUserId = new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8"),
                },
                new UserFollower
                {
                    Id = new Guid("3e37ab15-45eb-44ee-940a-59f87b407a8c"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                    FollowersUserId = new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8"),
                });
        }
    }
}
