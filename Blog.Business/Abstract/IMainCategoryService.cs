using Blog.Core.Results;
using Blog.Dto.MainCategory;

namespace Blog.Business.Abstract
{
    public interface IMainCategoryService
    {
        Task<Result> Get(Guid id);
        Task<Result> GetList();
        Task<Result> Add(MainCategoryAddDto mainCategoryAddDto);
        Task<Result> Delete(Guid id);
        Task<Result> Update(MainCategoryAddDto mainCategoryUpdateDto);
    }
}
