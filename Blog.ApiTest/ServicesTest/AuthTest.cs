using AutoMapper;
using Blog.APi.Controllers;
using Blog.Business.Abstract;
using Blog.Business.Concrete;
using Blog.Core.Results;
using Blog.Dto.Auth;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Blog.ApiTest.ServicesTest
{
    public class AuthTest
    {
        private readonly Mock<IAuthService> _authService;
        private readonly AuthController _controller;
        public AuthTest()
        {
            _authService = new Mock<IAuthService>();
            _controller = new AuthController(_authService.Object);
        }

        [Fact]
        public async void RegisterTest()
        {
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

            Result registerRes = new Result(true, "RegisterSuccess");
            _authService.Setup(s => s.Register(registerDto)).ReturnsAsync(registerRes);

            var result = await _controller.Register(registerDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<Result>(okResult.Value);
            Assert.True(returnedProduct.Success);
            Assert.Equal("RegisterSuccess", returnedProduct.Message);
        }
        [Fact]
        public async void LoginTest()
        {
            var loginUser = new LoginDto
            {            
                Email = "necil@necil.com",
                Password = "1234"
            };

            Result loginRes = new Result(false, "");
            _authService.Setup(s => s.Login(loginUser)).ReturnsAsync(loginRes);

            var result= await _controller.Login(loginUser);
            _authService.Verify(s => s.Login(loginUser), Times.Once);

            var badRequestResult = result as BadRequestObjectResult;
            var returnedProduct = Assert.IsType<Result>(badRequestResult.Value);
            Assert.False(returnedProduct.Success);

        }
        [Fact]
        public async void AccountConfirmTest()
        {
            Guid id = new();
            Result res = new Result(false, "");
            _authService.Setup(s => s.AccountConfirm(id)).ReturnsAsync(res);

            var result = await _controller.AccountConfirm(id.ToString());
            var badRequestResult = result as BadRequestObjectResult;
            var returnedProduct = Assert.IsType<Result>(badRequestResult.Value);
            Assert.False(returnedProduct.Success);

        }
    }
}
