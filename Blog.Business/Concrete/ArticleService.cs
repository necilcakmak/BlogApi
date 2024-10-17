using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Lang;
using Blog.Core.Results;
using Blog.Dto.Article;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Newtonsoft.Json;
using ServiceStack;

namespace Blog.Business.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LangService<Article> _lang;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lang = new LangService<Article>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Get(Guid id)
        {
            var article = await _unitOfWork.Articles.GetAsync(x => x.Id == id, x => x.User, x => x.Category.ParentCategory, x => x.Comments);

            if (article == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var articleDto = _mapper.Map<ArticleDto>(article);
            return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Add(ArticleAddDto articleAddDto)
        {
            Article article = _mapper.Map<Article>(articleAddDto);
            await _unitOfWork.Articles.AddAsync(article);
            await _unitOfWork.SaveAsync();

            var addedArticle = await _unitOfWork.Articles.GetAsync(x => x.Id == article.Id, x => x.Category.ParentCategory, x => x.User);
            var articleDto = _mapper.Map<ArticleDto>(addedArticle);
            return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Added));
        }

        public async Task<Result> GetList()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, x => x.Category.ParentCategory, x => x.User);

            var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
            return new DataResult<List<ArticleDto>>(articlesDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Delete(Guid id)
        {
            var articles = await _unitOfWork.Articles.GetAsync(x => x.Id == id);
            if (articles == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            await _unitOfWork.Articles.DeleteAsync(articles);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }

        public async Task<Result> Update(ArticleUpdateDto articleUpdateDto)
        {
            var articleInDb = await _unitOfWork.Articles.GetAsync(x => x.Id == articleUpdateDto.Id);
            if (articleInDb != null)
            {
                var jsonModel = JsonConvert.SerializeObject(articleUpdateDto);
                JsonConvert.PopulateObject(jsonModel, articleInDb);
                await _unitOfWork.Articles.UpdateAsync(articleInDb);
                await _unitOfWork.SaveAsync();
                var articleDto = _mapper.Map<ArticleDto>(articleInDb);
                return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Updated));
            }
            return new Result(false, _lang.Message(LangEnums.NotFound));

        }

        public async Task<Result> GetListMyArticle()
        {
            var articles = await _unitOfWork.Articles.GetMyArticleAsync();
            if (articles.Count <= 0)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var articlesDto = _mapper.Map<List<ArticleDto>>(articles);
            return new DataResult<List<ArticleDto>>(articlesDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> DeleteMyArticle(Guid id)
        {
            var res = await _unitOfWork.Articles.DeleteMyArticle(id);
            if (!res)
            {
                await _unitOfWork.DisposeAsync();
                return new Result(false, _lang.Message(LangEnums.NotDeleted));
            }
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }

        public async Task<Result> UpdateMyArticle(ArticleUpdateDto articleUpdateDto)
        {
            var articleInDb = await _unitOfWork.Articles.GetSelectedArticle(articleUpdateDto.Id);
            if (articleInDb == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var jsonModel = JsonConvert.SerializeObject(articleUpdateDto);
            JsonConvert.PopulateObject(jsonModel, articleInDb);
            await _unitOfWork.Articles.UpdateAsync(articleInDb);
            await _unitOfWork.SaveAsync();
            var articleDto = _mapper.Map<ArticleDto>(articleInDb);
            return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Updated));
        }

        public async Task<Result> DeleteList(List<Guid> idList)
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(x => idList.Contains(x.Id));
            if (articles == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            await _unitOfWork.Articles.DeleteListAsync(articles);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }
    }
}
