using Blog.Core.Results;
using Blog.Dto.ParentCategory;

namespace Blog.Business.Abstract
{
    public interface IParentCategoryService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetList();
        Task<Result> Add(ParentCategoryAddDto mainCategoryAddDto);
        Task<Result> Delete(Guid id);
        Task<Result> Update(ParentCategoryUpdateDto mainCategoryUpdateDto);
    }
}
