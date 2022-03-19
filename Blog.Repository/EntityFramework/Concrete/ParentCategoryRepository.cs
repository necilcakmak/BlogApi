using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class ParentCategoryRepository : EfRepositoryBase<ParentCategory>, IParentCategoryRepository
    {
        public ParentCategoryRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
        }
    }
}
