using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Lang;
using Blog.Core.Results;
using Blog.Dto.MainCategory;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;

namespace Blog.Business.Concrete
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LangService<ParentCategory> _lang;
        public MainCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lang = new LangService<ParentCategory>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Add(ParentCategoryAddDto parentCategoryAddDto)
        {
            bool inDb = await _unitOfWork.ParentCategories.AnyAsync(x => x.Name == parentCategoryAddDto.Name);
            if (inDb)
            {
                return new Result(false, _lang.Message(LangEnums.NameUsed));
            }
            ParentCategory mainCategory = _mapper.Map<ParentCategory>(parentCategoryAddDto);
            await _unitOfWork.ParentCategories.AddAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            var categoryDto = _mapper.Map<ParentCategoryDto>(mainCategory);
            return new DataResult<ParentCategoryDto>(categoryDto, true, _lang.Message(LangEnums.Added));
        }

        public async Task<Result> Delete(Guid id)
        {
            var mainCategory = await _unitOfWork.ParentCategories.GetAsync(x => x.Id == id);
            if (mainCategory == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            await _unitOfWork.ParentCategories.DeleteAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }

        public async Task<Result> Get(Guid id)
        {
            var mainCategory = await _unitOfWork.ParentCategories.GetAsync(x => x.Id == id);
            if (mainCategory == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var mainCategoryDto = _mapper.Map<ParentCategoryDto>(mainCategory);
            return new DataResult<ParentCategoryDto>(mainCategoryDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> GetList()
        {
            var mainCategories = await _unitOfWork.ParentCategories.GetAllAsync();
            if (mainCategories.Count <= 0)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var mainCategoriesDto = _mapper.Map<List<ParentCategoryDto>>(mainCategories);
            return new DataResult<List<ParentCategoryDto>>(mainCategoriesDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Update(ParentCategoryAddDto parentCategoryAddDto)
        {
            bool inDb = await _unitOfWork.ParentCategories.AnyAsync(x => x.Name == parentCategoryAddDto.Name);
            if (inDb)
            {
                return new Result(false, _lang.Message(LangEnums.NameUsed));
            }

            ParentCategory mainCategory = _mapper.Map<ParentCategory>(parentCategoryAddDto);
            await _unitOfWork.ParentCategories.UpdateAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            var mainCategoryDto = _mapper.Map<ParentCategoryDto>(mainCategory);
            return new DataResult<ParentCategoryDto>(mainCategoryDto, true, _lang.Message(LangEnums.Updated));
        }
    }
}
