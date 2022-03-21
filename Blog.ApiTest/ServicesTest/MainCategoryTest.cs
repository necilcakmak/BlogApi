using AutoMapper;
using Blog.Api.Controllers;
using Blog.APi.Controllers;
using Blog.ApiTest.TestSetup;
using Blog.Business.Concrete;
using Blog.Core.Results;
using Blog.Dto.Category;
using Blog.Dto.ParentCategory;
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
            var parentCategory = new ParentCategory { Name = categoryName };
            await _unitOfWork.ParentCategories.AddAsync(parentCategory);
            await _unitOfWork.SaveAsync();
            ParentCategoryAddDto mainCategoryAddDto = new()
            {
                Name = parentCategory.Name
            };

            //Act
            ParentCategoryService command = new(_unitOfWork, _mapper);
            var res = await command.Add(mainCategoryAddDto);

            //Assert
            res.Success.Should().BeFalse();
            res.Message.Should().Be("MainCategoryNameUsed");
        }

        [Fact]
        public async void WhenValidInputsAreGiven_MainCategory_ShouldBeCreated()
        {
            //Arrange
            ParentCategoryAddDto parentCategoryAddDto = new()
            {
                Name = "test name"
            };

            //Act
            ParentCategoryService command = new(_unitOfWork, _mapper);
            await command.Add(parentCategoryAddDto);

            //Assert
            var mainCategory = await _unitOfWork.ParentCategories.GetAllAsync();
            var category = mainCategory.Last();
            category.Should().NotBeNull();
            category.Name.Should().Be(parentCategoryAddDto.Name);
        }
    }
}
