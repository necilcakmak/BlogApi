using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Lang;
using Blog.Core.RabbitMQ;
using Blog.Core.Results;
using Blog.Core.Utilities;
using Blog.Core.Utilities.Abstract;
using Blog.Dto.User;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ServiceStack;
using System;

namespace Blog.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly LangService<User> _lng;
        private readonly IHashManager _hashManager;
        private readonly QueueFactory _rabbitMQPublisher;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IHashManager hashManager, QueueFactory rabbitMQPublisher)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
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
            var user = await _unitOfWork.Users.GetAsync(x => x.Id == id, x => x.UserSetting);
            if (user == null)
            {
                return new Result(false, _lng.Message(LangEnums.NotFound));
            }
            var userDto = _mapper.Map<UserDto>(user);
            return new DataResult<UserDto>(userDto, true, _lng.Message(LangEnums.Listed));
        }

        public async Task<Result> UpdateMyInformation(UserUpdateDto userUpdateDto, IFormFile imageFile, string webRootPath)
        {
            var user = await _unitOfWork.Users.GetMyUserInformation();
            if (userUpdateDto.PasswordIsChange && !_hashManager.Verify(userUpdateDto.OldPassword, user.Password))
                return new Result(false, _lng.Message(LangEnums.PasswordsDoNotMatch));



            user.FirstName = userUpdateDto.FirstName;
            user.LastName = userUpdateDto.LastName;
            user.Gender = userUpdateDto.Gender;
            if (userUpdateDto.PasswordIsChange)
                user.Password = _hashManager.Encrpt(userUpdateDto.Password);

            if (imageFile != null)
            {
                Delete(user.ImageName, webRootPath);
                user.ImageName = await SaveImage(imageFile, webRootPath);
            }

            var res = await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveAsync();
            UserDto userDto = _mapper.Map<UserDto>(res);
            return new DataResult<UserDto>(userDto, true, _lng.Message(LangEnums.Updated));
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

        public async Task<Result> SendNewPostMail()
        {
            var users = await _unitOfWork.Users.GetAllAsync(x => x.UserSetting.NewBlog == true);
            if (users.Count > 0)
            {
                _rabbitMQPublisher.Publish(users);
                return new Result(true, _lng.Message(LangEnums.MailSended));
            }
            return new Result(true, _lng.Message(LangEnums.Error));
        }

        public async Task<Result> GetList()
        {
            var user = await _unitOfWork.Users.GetAllAsync();
            if (user == null)
            {
                return new Result(false, _lng.Message(LangEnums.NotFound));
            }
            var userDto = _mapper.Map<List<UserDto>>(user);
            return new DataResult<List<UserDto>>(userDto, true, _lng.Message(LangEnums.Listed));
        }

        public static async Task<string> SaveImage(IFormFile imageFile, string webRootPath)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yyyymmssfff") + Path.GetExtension(imageFile.FileName);
            string FilePath = webRootPath + "\\images\\users";
            if (File.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            string imagePath = FilePath + "\\" + imageName;
            using FileStream stream = System.IO.File.Create(imagePath);
            await imageFile.CopyToAsync(stream);
            return imageName;
        }
        public static void Delete(string imageName, string webRootPath)
        {
            string FilePath = webRootPath + "\\images\\users\\" + imageName;
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}
