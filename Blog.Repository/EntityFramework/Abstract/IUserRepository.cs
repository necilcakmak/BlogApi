﻿using Blog.Core.Results;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.Base;

namespace Blog.Repository.EntityFramework.Abstract
{
    public interface IUserRepository : IEfRepositoryBase<User>
    {
        Task<User> GetMyUserInformation();
        Task<List<string>> GetFollowersMailList();
        Task<bool> UserApproved(Guid id);
    }
}
