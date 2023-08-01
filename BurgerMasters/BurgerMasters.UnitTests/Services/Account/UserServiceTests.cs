using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Auth;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Security.Claims;

namespace BurgerMasters.UnitTests.Services.Account
{
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepoMock;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _userRepoMock = new Mock<IUserRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            // Create a mock of the UserManager<ApplicationUser> class
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            _userService = new UserService(
                _userRepoMock.Object,
                _tokenServiceMock.Object,
                _httpContextAccessorMock.Object,
                _userManagerMock.Object);
        }

        [Test]
        public async Task Register_ValidData_ReturnsIdentityResult()
        {
            //Arrange
            string username = "Peter12";
            string email = "peter@abv.bg";
            string password = "password";
            string address = "Orehova gora number 20";

            DateTime birthdate = new DateTime(2003, 6, 29);

            RegisterViewModel model = new RegisterViewModel()
            {
                UserName = username,
                Email = email,
                Address = address,
                Password = password
            };

            var expectedResult = IdentityResult.Success;

            _userRepoMock.Setup(repo => repo.RegisterAsync(username, email, address, password, birthdate))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.RegisterAsync(model, birthdate);

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
            string address = "Orehova gora number 20";
            DateTime birthdate = new DateTime(2003, 6, 29);

            RegisterViewModel model = new RegisterViewModel()
            {
                UserName = username,
                Email = email,
                Address = address,
                Password = password
            };

            var expectedResult = IdentityResult.Failed(new IdentityError { Description = "Registration failed." }); ;

            _userRepoMock.Setup(repo => repo.RegisterAsync(username, email, address, password, birthdate))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.RegisterAsync(model, birthdate);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]

        public async Task LoginAsync_ValiData_ReturnsSignInResult()
        {
            //Arrange
            string email = "test@abv.bg";
            string password = "password123";

            LoginViewModel model = new LoginViewModel()
            {
                Email = email,
                Password = password
            };

            var expectedResult = SignInResult.Success;

            _userRepoMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.LoginAsync(model);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        public async Task LoginAsync_ValiData_ReturnsSignInResult_Failiure()
        {
            //Arrange
            string email = "test@abv.bg";
            string password = "password123";

            LoginViewModel model = new LoginViewModel()
            {
                Email = email,
                Password = password
            };

            var expectedResult = SignInResult.Failed;

            _userRepoMock.Setup(repo => repo.LoginAsync(email, password))
                .ReturnsAsync(expectedResult);

            //Act
            var result = await _userService.LoginAsync(model);

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