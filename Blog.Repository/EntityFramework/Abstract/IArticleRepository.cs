using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.Base;

namespace Blog.Repository.EntityFramework.Abstract
{
    public interface IArticleRepository : IEfRepositoryBase<Article>
    {
        Task<List<Article>> GetMyArticleAsync();
        Task<Article> GetSelectedArticle(Guid id);
        Task<bool> DeleteMyArticle(Guid id);
    }
}
