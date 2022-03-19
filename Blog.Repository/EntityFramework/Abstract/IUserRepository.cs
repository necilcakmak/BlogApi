using Blog.Core.Results;
using Blog.Dto;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.Base;

namespace Blog.Repository.EntityFramework.Abstract
{
    public interface IUserRepository : IEfRepositoryBase<User>
    {
        Task<User> GetMyUserInformation();
        Task<bool> UserApproved(Guid id);
    }
}
