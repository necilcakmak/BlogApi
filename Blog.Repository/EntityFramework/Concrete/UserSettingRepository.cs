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
    internal class UserSettingRepository : EfRepositoryBase<UserSetting>, IUserSettingRepository
    {
        BlogDbContext _dbContext;
        public UserSettingRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
            _dbContext = context;
        }

        public async Task<UserSetting> GetMySettings()
        {
            var user = await _dbContext.UserSettings.Where(x => x.UserId == UserId).FirstAsync();
            return user;
        }
    }
}
