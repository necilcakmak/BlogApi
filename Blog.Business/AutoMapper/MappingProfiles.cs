using AutoMapper;
using Blog.Dto.Article;
using Blog.Dto.Auth;
using Blog.Dto.Category;
using Blog.Dto.Comment;
using Blog.Dto.ParentCategory;
using Blog.Dto.User;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Context;

namespace Blog.Business.AutoMapper
{
    public class MappingProfiles : Profile
    {
        private static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }
        public MappingProfiles()
        {
            //auth mappings.
            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.ImageName, opt => opt.MapFrom(x => "DefaultUser.jpg"))
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(x => x.BirthDate.ToUniversalTime()));

            //auth mappings.
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Password, act => act.Ignore())
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow));

            //article mappings
            CreateMap<ArticleAddDto, Article>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => BlogDbContext.UserId));
            CreateMap<ArticleUpdateDto, Article>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow));
            CreateMap<Article, ArticleDto>();

            //category mappings
            CreateMap<CategoryAddDto, Category>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow));
            CreateMap<Category, CategoryDto>();

            //main category mappings
            CreateMap<ParentCategoryAddDto, ParentCategory>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow));
            CreateMap<ParentCategory, ParentCategoryDto>();

            //comment mappings
            CreateMap<CommentAddDto, Comment>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => BlogDbContext.UserId))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(x => false));
            CreateMap<Comment, CommentDto>();

            //user mappings
            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(x => DateTime.UtcNow))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(x => CalculateAge(x.BirthDate)))
                .ForMember(dest => dest.UserSetting, opt => opt.MapFrom(x => x.UserSetting));
            CreateMap<UserSetting, UserSettingDto>();
        }
    }
}
