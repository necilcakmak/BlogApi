using Blog.Dto;
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
            var user = await _dbContext.Users.Where(x => x.Id == UserId).Include(x => x.UserSetting).FirstAsync();
            return user;
        }

        public async Task<bool> UserApproved(Guid id)
        {
            var user = await _dbContext.Users.Where(x => x.Id == id).FirstAsync();
            if (user == null)
            {
                return false;
            }
            user.IsApproved = true;
            user.UpdatedDate = DateTime.UtcNow;
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
