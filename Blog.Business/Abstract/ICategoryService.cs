using Blog.Core.Results;
using Blog.Dto.Category;

namespace Blog.Business.Abstract
{
    public interface ICategoryService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetList();
        Task<Result> Add(CategoryAddDto categoryAddDto);
        Task<Result> Delete(Guid id);
        Task<Result> Update(CategoryAddDto categoryAddDto);
    }
}
