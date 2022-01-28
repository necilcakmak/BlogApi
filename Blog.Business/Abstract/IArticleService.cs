using Blog.Core.Results;
using Blog.Dto.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Abstract
{
    public interface IArticleService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetList(); 
         Task<Result> Add(ArticleAddDto articleAddDto);
        Task<Result> Delete(Guid id);
        Task<Result> Update(ArticleAddDto articleAddDto);
        Task<Result> UpdateMyArticle(ArticleUpdateDto articleUpdateDto);
        Task<Result> GetListMyArticle();
        Task<Result> DeleteMyArticle(Guid id);
    }
}
