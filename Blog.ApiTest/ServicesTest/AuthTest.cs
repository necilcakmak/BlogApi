using AutoMapper;
using Blog.Business.Abstract;
using Blog.Business.Abstract.RedisCache;
using Blog.Business.Concrete;
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
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Blog.ApiTest.ServicesTest
{
    public class AuthServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IHashManager> _hashManagerMock;
        private readonly Mock<IRedisService> _redisServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMailService> _mailServiceMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _hashManagerMock = new Mock<IHashManager>();
            _redisServiceMock = new Mock<IRedisService>();
            _mailServiceMock = new Mock<IMailService>();

            _authService = new AuthService(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _hashManagerMock.Object,
                _redisServiceMock.Object,
                _mailServiceMock.Object
            );
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsAccessToken()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "password" };
            var user = new User { Email = "test@test.com", Password = "hashedPassword" };
            _redisServiceMock.Setup(r => r.Set(It.IsAny<string>(), It.IsAny<User>()));

            _unitOfWorkMock.Setup(u => u.Users.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), null))
                           .ReturnsAsync(user);
            _hashManagerMock.Setup(h => h.Verify("password", "hashedPassword")).Returns(true);
            _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<User>()))
              .Returns((User u) => new UserDto { Email = u.Email });

            // Act
            var result = await _authService.Login(loginDto);

            // Assert
            Assert.True(result.Success);
            var dataResult = Assert.IsType<DataResult<AccessToken>>(result);
            Assert.NotNull(dataResult.Data);
            Assert.Equal(user.Email, dataResult.Data.User.Email);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsFailure()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@test.com", Password = "wrong" };

            _unitOfWorkMock.Setup(u => u.Users.GetAsync(It.IsAny<Expression<Func<User, bool>>>(), null))
                           .ReturnsAsync((User)null);

            // Act
            var result = await _authService.Login(loginDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("UserAccountNotFound", result.Message); // LangService mesajına göre
        }

        [Fact]
        public async Task Register_WhenEmailAndUsernameAreAvailable_ReturnsSuccess()
        {
            // Arrange
            var registerDto = new RegisterDto {Email = "test@test.com", UserName = "testuser", Password = "password", FirstName = "test", LastName = "test" };
            _unitOfWorkMock.Setup(u => u.Users.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync((User)null);
            _hashManagerMock.Setup(h => h.Encrpt(It.IsAny<string>())).Returns("hashedPassword");
            _mapperMock.Setup(m => m.Map<User>(registerDto)).Returns(new User { Email = registerDto.Email, UserName = registerDto.UserName });

            // Act
            var result = await _authService.Register(registerDto);

            // Assert
            Assert.True(result.Success);
            Assert.Equal("UserRegisterSuccess", result.Message); 
            _mailServiceMock.Verify(m => m.SenConfirmationMail(It.IsAny<MailDto>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Register_WhenEmailExists_ReturnsEmailAvailable()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@test.com", UserName = "testuser", Password = "password", FirstName = "test", LastName = "test" };
            var existingUser = new User { Email = "test@test.com", UserName = "otheruser" };

            _unitOfWorkMock.Setup(u => u.Users.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(existingUser);

            // Act
            var result = await _authService.Register(registerDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("UserEmailAvailable", result.Message);
        }

        [Fact]
        public async Task Register_WhenUsernameExists_ReturnsNicknameAvailable()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "other@test.com", UserName = "testuser", Password = "password", FirstName = "test", LastName = "test" };
            var existingUser = new User { Email = "another@test.com", UserName = "testuser" };

            _unitOfWorkMock.Setup(u => u.Users.GetAsync(It.IsAny<Expression<Func<User, bool>>>()))
                           .ReturnsAsync(existingUser);

            // Act
            var result = await _authService.Register(registerDto);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("UserNicknameAvailable", result.Message);
        }
    }
}
