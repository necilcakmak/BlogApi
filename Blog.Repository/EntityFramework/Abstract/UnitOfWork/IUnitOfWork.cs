using Blog.Repository.EntityFramework.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.EntityFramework.Abstract.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        IMainCategoryRepository MainCategories { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        IUserRepository Users { get; }
        IUserSettingRepository UserSettings { get; }
        IUserFollowerRepository UserFollower { get; }
        IUserFollowedRepository UserFollowed { get; }
        void Save();
        Task<int> SaveAsync();
    }
}
