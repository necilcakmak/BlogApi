using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Lang;
using Blog.Core.Results;
using Blog.Dto.Category;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;

namespace Blog.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LangService<Category> _lang;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lang = new LangService<Category>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Get(Guid id)
        {
            var category = await _unitOfWork.Categories.GetAsync(x => x.Id == id, x => x.ParentCategory);
            if (category == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new DataResult<CategoryDto>(categoryDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Add(CategoryAddDto categoryAddDto)
        {
            bool inDb = await _unitOfWork.Categories.AnyAsync(x => x.Name == categoryAddDto.CategoryName);
            if (inDb)
            {
                return new Result(false, _lang.Message(LangEnums.NameUsed));
            }
            Category category = _mapper.Map<Category>(categoryAddDto);
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new DataResult<CategoryDto>(categoryDto, true, _lang.Message(LangEnums.Added));
        }

        public async Task<Result> GetList()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, x => x.Articles, x => x.ParentCategory);
            if (categories.Count <= 0)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return new DataResult<List<CategoryDto>>(categoriesDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Delete(Guid id)
        {
            var category = await _unitOfWork.Categories.GetAsync(x => x.Id == id);
            if (category == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            await _unitOfWork.Categories.DeleteAsync(category);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }

        public async Task<Result> Update(CategoryAddDto categoryAddDto)
        {
            bool inDb = await _unitOfWork.Categories.AnyAsync(x => x.Name == categoryAddDto.CategoryName);
            if (inDb)
            {
                return new Result(false, _lang.Message(LangEnums.NameUsed));
            }

            Category category = _mapper.Map<Category>(categoryAddDto);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return new DataResult<CategoryDto>(categoryDto, true, _lang.Message(LangEnums.Updated));

        }
    }
}
