using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class UserFollowedRepository : EfRepositoryBase<UserFollowed>, IUserFollowedRepository
    {
        BlogDbContext _dbContext;
        public UserFollowedRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
            _dbContext = context;
        }

        public async Task<List<UserFollowed>> GetMyFolloweds()
        {
            var followeds = await _dbContext.UserFolloweds.Where(x => x.UserId == UserId).Include(x => x.User).ToListAsync();
            return followeds;
        }
    }
}
