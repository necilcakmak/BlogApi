using Blog.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Repository.EntityFramework.Mapping
{
    class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("uuid_generate_v4()");

            builder.Property(c => c.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.UpdatedDate).IsRequired().HasDefaultValue(DateTime.Now.ToUniversalTime());

            builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(a => a.Gender).IsRequired().HasDefaultValue(true);

            builder.Property(c => c.UserName).HasMaxLength(50);
            builder.Property(c => c.UserName).IsRequired();

            builder.Property(u => u.IsApproved).HasDefaultValue(false);

            builder.Property(c => c.UserSurname).HasMaxLength(50);
            builder.Property(c => c.UserSurname).IsRequired();

            builder.Property(c => c.NickName).HasMaxLength(50);
            builder.Property(c => c.NickName).IsRequired();
            builder.HasIndex(c => c.NickName).IsUnique();

            builder.Property(c => c.Email).HasMaxLength(50);
            builder.Property(c => c.Email).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique();

            builder.Property(c => c.Password).HasMaxLength(250);
            builder.Property(c => c.Password).IsRequired();

            builder.Property(c => c.BirthDate).IsRequired();
            builder.ToTable("Users");
            builder.HasData(
                new User
                {
                    Id = new Guid("c91266a4-35d3-4b60-89aa-6fa26c33c908"),
                    UserName = "Necil",
                    UserSurname = "Çakmak",
                    NickName = "necilcakmak",
                    Email = "necil@necil.com",
                    IsApproved = true,
                    Password = "$2a$11$wnQMJKF1vC6fAxs5IDaM1.5S3oMG.gEQMhON0bHUl5UQfe8v1AwIK",
                    Gender = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),
                    BirthDate = DateTime.Parse("1995-12-28").ToUniversalTime(),
                },
                new User
                {
                    Id = new Guid("45b533cd-ed21-4eb7-bb90-8838b6f9486c"),
                    CreatedDate = DateTime.Now.ToUniversalTime(),
                    UpdatedDate = DateTime.Now.ToUniversalTime(),
                    IsActive = true,
                    IsApproved = true,
                    UserName = "Ömer",
                    UserSurname = "Gürsoy",
                    NickName = "ömergürsoy",
                    Email = "ömer@ömer.com",
                    Password = "$2a$11$uNx/XA0odP6BAp8xKqtkausOYVPqmGNmq1GYK/y0E6OgQNb/7XIfC",
                    Gender = false,
                    BirthDate = DateTime.Parse("1990-11-18").ToUniversalTime()
                });
        }
    }
}
