using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract;
using Blog.Repository.EntityFramework.Concrete.Base;
using Blog.Repository.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.EntityFramework.Concrete
{
    public class ArticleRepository : EfRepositoryBase<Article>, IArticleRepository
    {
        BlogDbContext _blogDbContext;
        public ArticleRepository(BlogDbContext context) : base(context)
        {
            UserId = BlogDbContext.UserId;
            _blogDbContext = context;
        }

        public async Task<bool> DeleteMyArticle(Guid id)
        {
            var article = await _blogDbContext.Articles.Where(x => x.UserId == UserId && x.Id == id).FirstOrDefaultAsync();
            if (article != null)
            {
                await Task.Run(() => { _blogDbContext.Articles.Remove(article); });
                return true;
            }
            return false;
        }

        public async Task<List<Article>> GetMyArticleAsync()
        {
            var articles = await _blogDbContext.Articles.Where(x => x.UserId == UserId)
                .Include(x => x.Category)
                .ThenInclude(x => x.MainCategory)
                .Include(x => x.Comments)
                .ToListAsync();
            return articles;
        }

        public async Task<Article> GetSelectedArticle(Guid id)
        {
            var articleInDb = await _blogDbContext.Articles.Where(x => x.Id == id && x.UserId == UserId)
                .Include(x => x.User)
                .Include(x => x.Category).ThenInclude(x=>x.MainCategory)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync();
            return articleInDb;
        }
    }
}
