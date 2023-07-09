using System;
using System.Threading.Tasks;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace BurgerMasters.Infrastructure.Data.Tests
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private UserRepository _userRepository;
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
                _userManagerMock.Object, httpContextAccessorMock.Object, claimsPrincipalFactoryMock.Object, null, null, null, null);

            // Initialize the UserRepository with the mock instances
            _userRepository = new UserRepository(_userManagerMock.Object, _signInManagerMock.Object);
        }

        [Test]
        public async Task RegisterAsync_ValidData_ReturnsSuccess()
        {
            // Arrange
            var username = "testuser";
            var email = "testuser@example.com";
            var password = "P@ssw0rd";
            var birthdate = DateTime.Parse("1990-01-01");

            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _userRepository.RegisterAsync(username, email, password, birthdate);

            // Assert
            Assert.That(result, Is.EqualTo(IdentityResult.Success));
        }

        [Test]
        public async Task LoginAsync_ValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var email = "testuser@example.com";
            var password = "P@ssw0rd";

            var existingUser = new ApplicationUser { Email = email };

            _userManagerMock.Setup(x => x.FindByEmailAsync(email))
                .ReturnsAsync(existingUser);

            _signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<ApplicationUser>(), password,
                It.IsAny<bool>(), It.IsAny<bool>()))
                .ReturnsAsync(SignInResult.Success);

            // Act
            var result = await _userRepository.LoginAsync(email, password);

            // Assert
            Assert.That(result, Is.EqualTo(SignInResult.Success));
        }
    }
}