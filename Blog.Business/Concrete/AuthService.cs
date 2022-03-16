﻿using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Abstract.RedisCache;
using Blog.Business.Lang;
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
        private readonly ITokenHelper _tokenHelper;
        private readonly IMapper _mapper;
        private readonly IHashManager _hashManager;
        private readonly IMailService _mailService;
        private readonly LangService<User> _lng;
        public AuthService(IUnitOfWork unitOfWork, ITokenHelper tokenHelper, IMapper mapper, IHashManager hashManager, IMailService mailService)
        {
            _mailService = mailService;
            _lng = new LangService<User>();
            _hashManager = hashManager;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.GetAsync(x => x.Email == loginDto.Email || x.NickName == loginDto.Email);
            if (user != null && _hashManager.Verify(loginDto.Password, user.Password))
            {
                var token = _tokenHelper.CreateTokenUye(user);
                token.User = _mapper.Map<UserDto>(user);
                return new DataResult<AccessToken>(token, true, _lng.Message(LangEnums.LoginSuccess));
            }
            return new Result(false, _lng.Message(LangEnums.AccountNotFound));
        }

        public async Task<Result> Register(RegisterDto registerDto)
        {
            var userInDb = await _unitOfWork.Users.GetAsync(x => x.Email == registerDto.Email || x.NickName == registerDto.NickName);
            if (userInDb != null)
            {
                if (userInDb.Email == registerDto.Email)
                {
                    return new Result(false, _lng.Message(LangEnums.EmailAvailable));
                }
                else if (userInDb.NickName == registerDto.NickName)
                {
                    return new Result(false, _lng.Message(LangEnums.NicknameAvailable));
                }
            }

            User user = _mapper.Map<User>(registerDto);
            user.Password = _hashManager.Encrpt(user.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            await _unitOfWork.UserSettings.AddAsync(new UserSetting { UserId = user.Id });
            await _unitOfWork.SaveAsync();
            _mailService.SendMail(new MailDto { From = user.Email }, user.Id);
            return new Result(true, _lng.Message(LangEnums.MailSended));

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
