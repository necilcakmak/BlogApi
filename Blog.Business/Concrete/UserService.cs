using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Lang;
using Blog.Core.Results;
using Blog.Core.Utilities.Abstract;
using Blog.Dto.User;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Newtonsoft.Json;

namespace Blog.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LangService<User> _lng;
        private readonly IHashManager _hashManager;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHashManager hashManager)
        {
            _hashManager = hashManager;
            _lng = new LangService<User>();
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Delete(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(x => x.Id == id);
            if (user == null)
            {
                return new Result(false, _lng.Message(LangEnums.NotFound));
            }
            await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lng.Message(LangEnums.Deleted));
        }

        public async Task<Result> Get(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(x => x.Id == id);
            if (user == null)
            {
                return new Result(false, _lng.Message(LangEnums.NotFound));
            }
            var userDto = _mapper.Map<UserDto>(user);
            return new DataResult<UserDto>(userDto, true, _lng.Message(LangEnums.Listed));
        }

        public async Task<Result> UpdateMyInformation(UserUpdateDto userUpdateDto)
        {
            var user = await _unitOfWork.Users.GetMyUserInformation();
            //password change is true and new password and old password is equal ?
            if (userUpdateDto.PasswordIsChange && !_hashManager.Verify(userUpdateDto.OldPassword, user.Password))
            {
                return new Result(false, _lng.Message(LangEnums.PasswordsDoNotMatch));
            }

            var jsonModel = JsonConvert.SerializeObject(userUpdateDto);
            JsonConvert.PopulateObject(jsonModel, user);

            if (userUpdateDto.PasswordIsChange)
            {
                user.Password = _hashManager.Encrpt(userUpdateDto.Password);
            }

            var updatedUser = await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            UserDto userDto = _mapper.Map<UserDto>(updatedUser);
            return new DataResult<UserDto>(userDto, true, _lng.Message(LangEnums.Listed));
        }

        public async Task<Result> UpdateMySettings(UserSettingDto userSettingDto)
        {
            var userSetting = await _unitOfWork.UserSettings.GetMySettings();
            userSetting.NewBlog = userSettingDto.NewBlog;
            userSetting.ReceiveMail = userSettingDto.ReceiveMail;
            await _unitOfWork.UserSettings.UpdateAsync(userSetting);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lng.Message(LangEnums.Updated));
        }

        public async Task<Result> UserInformation()
        {
            var user = await _unitOfWork.Users.GetMyUserInformation();
            UserDto userDto = _mapper.Map<UserDto>(user);
            return new DataResult<UserDto>(userDto, true, _lng.Message(LangEnums.Listed));
        }
        public async Task<Result> Follow(UserFollowDto userFollowDto)
        {
            var userFollower = _mapper.Map<UserFollower>(userFollowDto);
            var userFollowed = _mapper.Map<UserFollowed>(userFollowDto);
            await _unitOfWork.UserFollower.AddAsync(userFollower);
            await _unitOfWork.UserFollowed.AddAsync(userFollowed);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lng.Message(LangEnums.Added));
        }

        public async Task<Result> UnFollow(UserFollowDto userFollowDto)
        {
            var userFollower = _mapper.Map<UserFollower>(userFollowDto);
            var userFollowed = _mapper.Map<UserFollowed>(userFollowDto);
            await _unitOfWork.UserFollower.DeleteAsync(userFollower);
            await _unitOfWork.UserFollowed.DeleteAsync(userFollowed);
            await _unitOfWork.SaveAsync();
            return new Result(true, _lng.Message(LangEnums.Deleted));
        }

        public async Task<Result> GetFollow()
        {
            var followeds = await _unitOfWork.UserFollowed.GetMyFolloweds();
            var followers = await _unitOfWork.UserFollower.GetMyFollowers();
            var followersDto = _mapper.Map<List<UserFollowersDto>>(followers);
            var followedsDto = _mapper.Map<List<UserFollowedsDto>>(followeds);
            FollowAndFollowersDto followAndFollowersDto = new()
            {
                UserFollowed = followedsDto,
                UserFollower = followersDto
            };
            return new DataResult<FollowAndFollowersDto>(followAndFollowersDto, true, _lng.Message(LangEnums.Listed));
        }
    }
}
