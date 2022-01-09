using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class UserRepository : EfRepositoryBase<User>, IUserRepository
    {
        public UserRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
        }

        public async Task<User> GetMyUserInformation()
        {
            IQueryable<User> query = _context.Set<User>();
            query = query.Where(x => x.Id == UserId);
            return await query.FirstOrDefaultAsync();
        }
    }
}
