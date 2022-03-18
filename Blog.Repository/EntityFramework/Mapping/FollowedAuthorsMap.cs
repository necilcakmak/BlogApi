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
    class FollowedAuthorsMap : IEntityTypeConfiguration<UserFollowed>
    {
        public void Configure(EntityTypeBuilder<UserFollowed> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.FollowedUserId).IsRequired().HasColumnType("uuid");

            builder.Property(c => c.FollowedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.HasOne(a => a.User).WithMany(c => c.UserFollowed).HasForeignKey(a => a.FollowedUserId);

            builder.ToTable("UserFolloweds");
            builder.HasData(
                new UserFollowed
                {
                    Id = new Guid("7cd06ada-c8ce-4d70-9764-d7fe138bdd9c"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                    FollowedUserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                },
                new UserFollowed
                {
                    Id = new Guid("9955ae8d-c9ff-41b8-84c1-12cd18770fed"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8"),
                    FollowedUserId = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                },
                new UserFollowed
                {
                    Id = new Guid("e9884b3f-6665-42b4-9b67-1555ce3b4f94"),
                    FollowedDate = DateTime.Now.ToUniversalTime(),
                    UserId = new Guid("30d00d67-4f1e-405f-a992-f9ef825550c8"),
                    FollowedUserId = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                });
        }
    }
}
