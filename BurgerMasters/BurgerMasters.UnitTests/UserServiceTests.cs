using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;

namespace BurgerMasters.Core.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _userService = new UserService(
                _userRepositoryMock.Object,
                _tokenServiceMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        [Test]
        public async Task RegisterAsync_ValidData_ReturnsIdentityResult()
        {
            // Arrange
            string username = "testuser";
            string email = "test@example.com";
            string password = "password";
            DateTime birthdate = new DateTime(1990, 1, 1);
            var expectedResult = IdentityResult.Success;
            _userRepositoryMock.Setup(repo => repo.RegisterAsync(username, email, password, birthdate))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _userService.RegisterAsync(username, email, password, birthdate);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task LoginAsync_ValidData_ReturnsSignInResult()
        {
            // Arrange
            string email = "test@example.com";
            string password = "password";
            var expectedResult = SignInResult.Success;
            _userRepositoryMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _userService.LoginAsync(email, password);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task LogoutAsync_CallsHttpContextAccessor()
        {
            // Arrange
            var httpContextMock = new Mock<HttpContext>();
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            httpContextMock.SetupGet(c => c.User).Returns(claimsPrincipal);
            _httpContextAccessorMock.SetupGet(a => a.HttpContext).Returns(httpContextMock.Object);

            // Act
            await _userService.LogoutAsync();

            // Assert
            httpContextMock.VerifySet(c => c.User = It.IsAny<ClaimsPrincipal>(), Times.Once);
        }

        /*[Test]
        public void SetUserIdentity_CallsHttpContextAccessor()
        {
            // Arrange
            var userInfo = new ExportUserDto
            {
                Username = "testuser",
                Email = "test@example.com",
                Birthday = "1990-01-01"
            };
            string userId = "12345";
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var userIdentity = new ClaimsIdentity(claims);
            var claimPrincipal = new ClaimsPrincipal(userIdentity);

            // Create a mock HttpContext
            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.SetupGet(c => c.User).Returns(new ClaimsPrincipal());

            // Configure the IHttpContextAccessor mock to return the mock HttpContext
            _httpContextAccessorMock.SetupGet(a => a.HttpContext).Returns(httpContextMock.Object);

            _tokenServiceMock.Setup(service => service.GetClaims(userInfo, userId)).Returns(claims);

            // Act
            _userService.SetUserIdentity(userInfo, userId);

            // Assert
            httpContextMock.VerifySet(c => c.User = claimPrincipal, Times.Once);
        }*/
    }
}