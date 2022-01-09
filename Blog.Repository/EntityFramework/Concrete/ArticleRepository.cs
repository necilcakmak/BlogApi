using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class ArticleRepository : EfRepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
        }

        public async Task<bool> DeleteMyArticle(Guid id)
        {
            IQueryable<Article> query = _context.Set<Article>();
            query = query.Where(x => x.UserId == UserId && x.Id == id);
            var article = await query.FirstOrDefaultAsync();
            if (article != null)
            {
                await Task.Run(() => { _context.Set<Article>().Remove(article); });
                return true;
            }
            return false;

        }

        public async Task<List<Article>> GetMyArticleAsync()
        {
            IQueryable<Article> query = _context.Set<Article>();
            query = query.Where(x => x.UserId == UserId).Include(x => x.Category).ThenInclude(x => x.MainCategory).Include(x => x.Comments);
            return await query.ToListAsync();
        }
    }
}
