using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.UnitTests.User
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private UserService _userService;

        [SetUp]

        public void Setup()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _userService = new UserService(
                _userRepoMock.Object,
                _tokenServiceMock.Object,
                _httpContextAccessorMock.Object);
        }

        [Test]
        public async Task Register_ValidData_ReturnsIdentityResult()
        {
            //Arrange
            string username = "Peter12";
            string email = "peter@abv.bg";
            string password = "password";
            DateTime birthdate = new DateTime(2003, 6, 29);

            var expectedResult = IdentityResult.Success;

            _userRepoMock.Setup(repo => repo.RegisterAsync(username, email, password, birthdate))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.RegisterAsync(username, email, password, birthdate);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task Register_ValidData_ReturnsIdentityResult_Failure()
        {
            //Arrange
            string username = "Peter12";
            string email = "peter@abv.bg";
            string password = "password";
            DateTime birthdate = new DateTime(2003, 6, 29);

            var expectedResult = IdentityResult.Failed(new IdentityError { Description = "Registration failed." }); ;

            _userRepoMock.Setup(repo => repo.RegisterAsync(username, email, password, birthdate))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.RegisterAsync(username, email, password, birthdate);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]

        public async Task LoginAsync_ValiData_ReturnsSignInResult()
        {
            //Arrange
            string email = "test@abv.bg";
            string password = "password123";

            var expectedResult = SignInResult.Success;

            _userRepoMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.LoginAsync(email, password);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        public async Task LoginAsync_ValiData_ReturnsSignInResult_Failiure()
        {
            //Arrange
            string email = "test@abv.bg";
            string password = "password123";

            var expectedResult = SignInResult.Failed;

            _userRepoMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.LoginAsync(email, password);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task LogoutAsync_CallsHttpContextAccessor()
        {
            //Arrange
            var httpContextMock = new Mock<HttpContext>();
            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            httpContextMock.SetupGet(c => c.User).Returns(claimPrincipal);

            _httpContextAccessorMock.SetupGet(a => a.HttpContext)
                .Returns(httpContextMock.Object);

            //Act
            await _userService.LogoutAsync();

            //Asssert
            httpContextMock.VerifySet(c => c.User = It.IsAny<ClaimsPrincipal>(), Times.Once);
        }
    }
}
