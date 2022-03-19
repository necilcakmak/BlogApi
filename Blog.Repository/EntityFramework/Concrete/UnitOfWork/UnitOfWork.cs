using Blog.Core.Utilities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.EntityFramework.Concrete.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public BlogDbContext _blogDbContext;
        private UserRepository _userRepository;
        private ArticleRepository _articleRepository;
        private CommentRepository _commentRepository;
        private CategoryRepository _categoryRepository;
        private MainCategoryRepository _mainCategoryRepository;
        private UserSettingRepository _userSettingRepository;
        private UserFollowerRepository _userFollowerRepository;
        private UserFollowedRepository _userFollowedRepository;
        public UnitOfWork(BlogDbContext blogDbContext)
        {
            _blogDbContext = blogDbContext;
        }
        public IArticleRepository Articles => _articleRepository ?? new ArticleRepository(_blogDbContext);
        public IMainCategoryRepository MainCategories => _mainCategoryRepository ?? new MainCategoryRepository(_blogDbContext);
        public ICategoryRepository Categories => _categoryRepository ?? new CategoryRepository(_blogDbContext);
        public ICommentRepository Comments => _commentRepository ?? new CommentRepository(_blogDbContext);
        public IUserRepository Users => _userRepository ?? new UserRepository(_blogDbContext);
        public IUserSettingRepository UserSettings => _userSettingRepository ?? new UserSettingRepository(_blogDbContext);

        public IUserFollowerRepository UserFollower => _userFollowerRepository ?? new UserFollowerRepository(_blogDbContext);

        public IUserFollowedRepository UserFollowed => _userFollowedRepository ?? new UserFollowedRepository(_blogDbContext);

        public async ValueTask DisposeAsync()
        {
            await _blogDbContext.DisposeAsync();
        }

        public void Save()
        {
            var entries = _blogDbContext.ChangeTracker.Entries().Where(E => E.State == EntityState.Added || E.State == EntityState.Modified).ToList();
            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(BaseEntityType.UpdatedDate).CurrentValue = DateTime.Now.ToUniversalTime();
                }
            }
            _blogDbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            var entries = _blogDbContext.ChangeTracker.Entries().Where(E => E.State == EntityState.Added || E.State == EntityState.Modified).ToList();
            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Property(BaseEntityType.UpdatedDate).CurrentValue = DateTime.UtcNow;
                }
            }
            return await _blogDbContext.SaveChangesAsync();
        }
    }
}
