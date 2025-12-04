using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Abstract.RedisCache;
using Blog.Business.Lang;
using Blog.Core.RabbitMQ;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Core.Utilities.Abstract;
using Blog.Dto.Auth;
using Blog.Dto.Token;
using Blog.Dto.User;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;

namespace Blog.Business.Concrete
{
    public class AuthService : IAuthService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHashManager _hashManager;
        private readonly LangService<User> _lng;
        private readonly IRedisService _redisService;
        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IHashManager hashManager, IRedisService redisService, QueueFactory queueFactory)
        {
            _redisService = redisService;
            _lng = new LangService<User>();
            _hashManager = hashManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetAsync(x => x.Email == loginDto.Email || x.UserName == loginDto.Email, x => x.UserSetting);
            if (user != null && _hashManager.Verify(loginDto.Password, user.Password))
            {
                var key = Guid.NewGuid().ToString();
                AccessToken accessToken = user.CreateTokenUye(key);
                _redisService.Set(key, user);
                var userDto = _mapper.Map<UserDto>(user);
                accessToken.User = userDto;
                return new DataResult<AccessToken>(accessToken, true, _lng.Message(LangEnums.LoginSuccess));
            }
            return new Result(false, _lng.Message(LangEnums.AccountNotFound));
        }

        public async Task<Result> Register(RegisterDto registerDto)
        {
            var userInDb = await _unitOfWork.Users.GetAsync(x => x.Email == registerDto.Email || x.UserName == registerDto.UserName);
            if (userInDb != null)
            {
                if (userInDb.Email == registerDto.Email)
                {
                    return new Result(false, _lng.Message(LangEnums.EmailAvailable));
                }
                else if (userInDb.UserName == registerDto.UserName)
                {
                    return new Result(false, _lng.Message(LangEnums.NicknameAvailable));
                }
            }

            User user = _mapper.Map<User>(registerDto);
            user.Password = _hashManager.Encrpt(user.Password);
            await _unitOfWork.Users.AddAsync(user);
            user.UserSetting = new() { UserId = user.Id };
            await _unitOfWork.SaveAsync();
            return new Result(true, _lng.Message(LangEnums.RegisterSuccess));

        }

        public async Task<Result> AccountConfirm(Guid id)
        {
            bool res = await _unitOfWork.Users.UserApproved(id);
            if (!res)
            {
                return new Result(false, _lng.Message(LangEnums.AccountNotFound));
            }
            return new Result(true, _lng.Message(LangEnums.AccountConfirmed));
        }
    }
}
