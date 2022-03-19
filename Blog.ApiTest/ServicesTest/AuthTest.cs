using AutoMapper;
using Blog.ApiTest.TestSetup;
using Blog.Business.Concrete;
using Blog.Dto.Auth;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using FluentAssertions;
using System;
using Xunit;

namespace Blog.ApiTest.ServicesTest
{
    public class AuthTest : IClassFixture<CommonTestFixture>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AuthTest(CommonTestFixture testFixture)
        {
            _unitOfWork = testFixture.UnitOfWork;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public async void WhenAlreadyExistUserEmailIsGiven_EmailUsed_ShouldBeReturn()
        {
            //Arrange
            var user = new User
            {
                FirstName = "necil",
                Email = "necil@necil.com",
                Gender = true,
                Password = "1234",
                UserName = "necilcakmak",
                LastName = "çakmak",
                BirthDate = DateTime.Now.ToUniversalTime()
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveAsync();
            RegisterDto registerDto = new()
            {
                UserName = user.UserName,
                Email = user.Email,
                Gender = true,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate
            };

            //Act
            AuthService command = new(_unitOfWork, _mapper, null, null, null);
            var res = await command.Register(registerDto);

            //Assert
            res.Success.Should().BeFalse();
            res.Message.Should().Be("UserEmailAvailable");
        }
    }
}
