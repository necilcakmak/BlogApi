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
        private readonly LangService<MainCategory> _lang;
        public MainCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _lang = new LangService<MainCategory>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Add(MainCategoryAddDto mainCategoryAddDto)
        {
            bool inDb = await _unitOfWork.MainCategories.AnyAsync(x => x.MainCategoryName == mainCategoryAddDto.MainCategoryName);
            if (inDb)
            {
                return new Result(false, _lang.Message(LangEnums.NameUsed));
            }
            MainCategory mainCategory = _mapper.Map<MainCategory>(mainCategoryAddDto);
            await _unitOfWork.MainCategories.AddAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            var categoryDto = _mapper.Map<MainCategoryDto>(mainCategory);
            return new DataResult<MainCategoryDto>(categoryDto, true, _lang.Message(LangEnums.Added));
        }

        public async Task<Result> Delete(Guid id)
        {
            var mainCategory = await _unitOfWork.MainCategories.GetAsync(x => x.Id == id);
            if (mainCategory == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            await _unitOfWork.MainCategories.DeleteAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lang.Message(LangEnums.Deleted));
        }

        public async Task<Result> Get(Guid id)
        {
            var mainCategory = await _unitOfWork.MainCategories.GetAsync(x => x.Id == id);
            if (mainCategory == null)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var mainCategoryDto = _mapper.Map<MainCategoryDto>(mainCategory);
            return new DataResult<MainCategoryDto>(mainCategoryDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> GetList()
        {
            var mainCategories = await _unitOfWork.MainCategories.GetAllAsync();
            if (mainCategories.Count <= 0)
            {
                return new Result(false, _lang.Message(LangEnums.NotFound));
            }
            var mainCategoriesDto = _mapper.Map<List<MainCategoryDto>>(mainCategories);
            return new DataResult<List<MainCategoryDto>>(mainCategoriesDto, true, _lang.Message(LangEnums.Listed));
        }

        public async Task<Result> Update(MainCategoryAddDto mainCategoryUpdateDto)
        {
            bool inDb = await _unitOfWork.MainCategories.AnyAsync(x => x.MainCategoryName == mainCategoryUpdateDto.MainCategoryName);
            if (inDb)
            {
                return new Result(false, _lang.Message(LangEnums.NameUsed));
            }

            MainCategory mainCategory = _mapper.Map<MainCategory>(mainCategoryUpdateDto);
            await _unitOfWork.MainCategories.UpdateAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            var mainCategoryDto = _mapper.Map<MainCategoryDto>(mainCategory);
            return new DataResult<MainCategoryDto>(mainCategoryDto, true, _lang.Message(LangEnums.Updated));
        }
    }
}
