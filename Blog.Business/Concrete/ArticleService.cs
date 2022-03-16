using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Abstract.RabbitMQ;
using Blog.Business.Lang;
using Blog.Core.Results;
using Blog.Dto.Article;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Newtonsoft.Json;

namespace Blog.Business.Concrete
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LangService<Article> _lang;
        private readonly IRabbitMQPublisher _rabbitMQPublisher;
        public ArticleService(IUnitOfWork unitOfWork, IMapper mapper, IRabbitMQPublisher rabbitMQPublisher)
        {
            _lang = new LangService<Article>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public async Task<Result> Get(Guid id)
        {
            var article = await _unitOfWork.Articles.GetAsync(x => x.Id == id, x => x.User, x => x.Category.MainCategory, x => x.Comments);

            if (article == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var articleDto = _mapper.Map<ArticleDto>(article);
            return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Add(ArticleAddDto articleAddDto)
        {
            try
            {

                Article article = _mapper.Map<Article>(articleAddDto);
                await _unitOfWork.Articles.AddAsync(article);
                await _unitOfWork.SaveAsync();
                #region rabbitmq publisher service send mail
                var userFollwersUser = await _unitOfWork.Users.GetFollowersMailList();
                _rabbitMQPublisher.Publish(userFollwersUser);
                #endregion
                var articleDto = _mapper.Map<ArticleDto>(article);
                return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Added));

            }
            catch (Exception ex)
            {
                var a = ex.Message;
                throw;
            }
        }

        public async Task<Result> GetList()
        {
            var articles = await _unitOfWork.Articles.GetAllAsync(null, x => x.Category.MainCategory, x => x.User);
            if (articles.Count <= 0)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
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

        public async Task<Result> Update(ArticleAddDto articleAddDto)
        {
            Article article = _mapper.Map<Article>(articleAddDto);
            await _unitOfWork.Articles.UpdateAsync(article);
            await _unitOfWork.SaveAsync();
            var articleDto = _mapper.Map<ArticleDto>(article);
            return new DataResult<ArticleDto>(articleDto, true, _lang.Message(LangEnums.Updated));
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
    }
}
