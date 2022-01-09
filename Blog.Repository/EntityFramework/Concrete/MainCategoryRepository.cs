using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class MainCategoryRepository : EfRepositoryBase<MainCategory>, IMainCategoryRepository
    {
        public MainCategoryRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
        }
    }
}
