using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BurgerMasters.UnitTests.Services.Account
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private UserRepository _userRepo;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private Mock<SignInManager<ApplicationUser>> _signInManagerMock;

        [SetUp]
        public void Setup()
        {
            // Create mock instances of UserManager, IHttpContextAccessor, and IUserClaimsPrincipalFactory
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                _userManagerMock.Object, httpContextAccessorMock.Object,
                claimsPrincipalFactoryMock.Object, null, null, null, null);

            // Initialize the UserRepository with the mock instances
            _userRepo = new UserRepository(_userManagerMock.Object, _signInManagerMock.Object);
        }

        [Test]
        public async Task RegisterAsync_ValiData_ReturnSuccess()
        {
            //Arrange
            string username = "Kiril20";
            string email = "kiril@abv.bg";
            string password = "KR#20%2003";
            string address = "Orehova gora number 20";
            DateTime birthdate = DateTime.Parse("2003-03-15");

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            //Act
            var result = await _userRepo.RegisterAsync(username, email, address, password, birthdate);

            //Assert
            Assert.That(result, Is.EqualTo(IdentityResult.Success));
        }

        [Test]
        public async Task RegisterAsync_ValidInput_ReturnsFailedIdentityResult()
        {
            //Arrange
            string username = "Kiril10";
            string email = "kiro@abv.com";
            string password = "Ki12$24%2003";
            string address = "Orehova gora number 20";
            DateTime birthdate = new DateTime(2000, 1, 1);

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "Error", Description = "Failed to create user." }));

            // Act
            var result = await _userRepo.RegisterAsync(username, email, address, password, birthdate);

            // Assert
            Assert.IsFalse(result.Succeeded);
            Assert.AreEqual("Failed to create user.", result.Errors.First().Description);
        }

        [Test]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
        {
            //Arragne
            var email = "Kiril10";
            var password = "Ki12$24%2003";

            var existingUser = new ApplicationUser
            {
                Email = email,
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(existingUser);

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), password,
                It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            //Act
            var result = await _userRepo.LoginAsync(email, password);

            //Assert
            Assert.That(result, Is.EqualTo(SignInResult.Success));
        }

        [Test]
        public async Task LoginAsync_ValidCredentials_ReturnsFailed()
        {
            //Arragne
            var email = "test@abv.bg";
            var password = "testPass";

            var existingUser = new ApplicationUser
            {
                Email = email,
            };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(existingUser);

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), password,
                It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Failed);

            //Act
            var result = await _userRepo.LoginAsync(email, password);

            //Assert
            Assert.That(result, Is.EqualTo(SignInResult.Failed));
        }
    }
}