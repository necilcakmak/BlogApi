using Blog.Core.Results;
using Blog.Dto.Comment;

namespace Blog.Business.Abstract
{
    public interface ICommentService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetList();
        Task<Result> Add(CommentAddDto commentAddDto);
        Task<Result> Delete(Guid id);
        Task<Result> Update(CommentUpdateDto commentUpdateDto);
        Task<Result> DeleteMyComment(Guid id);

    }
}
