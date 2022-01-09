using Blog.Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Context
{
    public interface IBlogDbContext
    {
        static Guid UserId { get; set; } 
        DbSet<User> Users { get; set; }
        DbSet<Article> Articles { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<MainCategory> MainCategories { get; set; }
        int SaveChanges();

    }
}
