using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Lang;
using Blog.Core.Results;
using Blog.Dto.Comment;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;

namespace Blog.Business.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LangService<Comment> _lang;
        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lang = new LangService<Comment>();
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result> Get(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetAsync(x => x.Id == id);
            if (comment == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var commentDto = _mapper.Map<CommentDto>(comment);
            return new DataResult<CommentDto>(commentDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Add(CommentAddDto commentAddDto)
        {
            Comment comment = _mapper.Map<Comment>(commentAddDto);
            await _unitOfWork.Comments.AddAsync(comment);
            await _unitOfWork.SaveAsync();
            var commentDto = _mapper.Map<CommentDto>(comment);
            return new DataResult<CommentDto>(commentDto, true, _lang.Message(LangEnums.Added));
        }

        public async Task<Result> GetList()
        {
            var comments = await _unitOfWork.Comments.GetAllAsync(null, x => x.Article, x => x.User);
            if (comments.Count <= 0)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var commentsDto = _mapper.Map<List<CommentDto>>(comments);
            return new DataResult<List<CommentDto>>(commentsDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Delete(Guid id)
        {
            var comment = await _unitOfWork.Comments.GetAsync(x => x.Id == id);
            if (comment == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            await _unitOfWork.Comments.DeleteAsync(comment);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }

        public async Task<Result> Update(CommentUpdateDto commentUpdateDto)
        {
            var comment = await _unitOfWork.Comments.GetAsync(x => x.Id == commentUpdateDto.Id);
            if (comment != null)
            {
                comment.Text = commentUpdateDto.Text;
                await _unitOfWork.Comments.UpdateAsync(comment);
                await _unitOfWork.SaveAsync();
                var commentDto = _mapper.Map<CommentDto>(comment);
                return new DataResult<CommentDto>(commentDto, true, _lang.Message(LangEnums.Updated));
            }
            return new Result(false, _lang.Message(LangEnums.NotFound));
        }

        public async Task<Result> DeleteMyComment(Guid id)
        {
            var comment = await _unitOfWork.Comments.DeleteMyComment(id);
            if (!comment)
            {
                await _unitOfWork.DisposeAsync();
                return new Result(false, _lang.Message(LangEnums.NotDeleted));
            }
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }
    }
}
