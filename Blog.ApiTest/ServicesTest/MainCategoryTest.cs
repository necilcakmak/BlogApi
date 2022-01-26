using AutoMapper;
using Blog.Api.Controllers;
using Blog.APi.Controllers;
using Blog.ApiTest.TestSetup;
using Blog.Business.Concrete;
using Blog.Core.Results;
using Blog.Dto.Category;
using Blog.Dto.MainCategory;
using Blog.Entities.Entities;
using Blog.Repository.EntityFramework.Abstract.UnitOfWork;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Xunit;

namespace Blog.ApiTest.ServicesTest
{
    public class MainCategoryTest : IClassFixture<CommonTestFixture>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MainCategoryTest(CommonTestFixture testFixture)
        {
            _unitOfWork = testFixture.UnitOfWork;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("Bilim")]
        [InlineData("Kültür")]
        public async void WhenAlreadyExistCategoryNameIsGiven_MainCategoryNameUsed_ShouldBeReturn(string categoryName)
        {
            //Arrange
            var mainCategory = new MainCategory { MainCategoryName = categoryName };
            await _unitOfWork.MainCategories.AddAsync(mainCategory);
            await _unitOfWork.SaveAsync();
            MainCategoryAddDto mainCategoryAddDto = new()
            {
                MainCategoryName = mainCategory.MainCategoryName
            };

            //Act
            MainCategoryService command = new(_unitOfWork, _mapper);
            var res = await command.Add(mainCategoryAddDto);

            //Assert
            res.Success.Should().BeFalse();
            res.Message.Should().Be("MainCategoryNameUsed");
        }

        [Fact]
        public async void WhenValidInputsAreGiven_MainCategory_ShouldBeCreated()
        {
            //Arrange
            MainCategoryAddDto mainCategoryAddDto = new()
            {
                MainCategoryName = "test name"
            };

            //Act
            MainCategoryService command = new(_unitOfWork, _mapper);
            await command.Add(mainCategoryAddDto);

            //Assert
            var mainCategory = await _unitOfWork.MainCategories.GetAllAsync();
            var category = mainCategory.Last();
            category.Should().NotBeNull();
            category.MainCategoryName.Should().Be(mainCategoryAddDto.MainCategoryName);
        }
    }
}
