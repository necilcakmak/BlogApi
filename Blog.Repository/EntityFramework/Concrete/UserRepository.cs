using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class UserRepository : EfRepositoryBase<User>, IUserRepository
    {
        BlogDbContext _dbContext;
        public UserRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
            _dbContext = context;
        }

        public async Task<User> GetMyUserInformation()
        {
            var user = await _dbContext.Users.Where(x => x.Id == UserId).FirstAsync();
            return user;
        }
    }
}
