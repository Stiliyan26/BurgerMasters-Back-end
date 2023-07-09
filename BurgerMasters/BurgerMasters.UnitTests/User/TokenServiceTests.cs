using BurgerMasters.Core.Models;
using BurgerMasters.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.UnitTests.User
{
    [TestFixture]
    public class TokenServiceTests
    {
        private TokenService _tokenService;
        private Mock<IConfiguration> _configurationMock;

        [SetUp]
        public void Setup()
        {
            _configurationMock = new Mock<IConfiguration>();
            _tokenService = new TokenService(_configurationMock.Object);
        }

        [Test]
        public void GenerateToken_ReturnsValidJwtToken()
        {
            // Arrange
            var userInfo = new ExportUserDto
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Birthdate = DateTime.Now.ToString()
            };
            var userId = "12345";

            _configurationMock.SetupGet(c => c["Jwt:Key"]).Returns("mysecretkey123dasdad3e23dadasda");
            _configurationMock.SetupGet(c => c["Jwt:Issuer"]).Returns("myissuer");
            _configurationMock.SetupGet(c => c["Jwt:Audience"]).Returns("myaudience");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretkey123dasdad3e23dadasda"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = _tokenService.GenerateToken(userInfo, userId);

            // Act
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "myissuer",
                ValidAudience = "myaudience",
                IssuerSigningKey = securityKey
            };

            // Assert
            Assert.NotNull(token);
            Assert.IsNotEmpty(token);

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out _);
            var claimsIdentity = claimsPrincipal.Identity as ClaimsIdentity;

            Assert.AreEqual(userId, claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Assert.AreEqual(userInfo.Email, claimsIdentity.FindFirst(ClaimTypes.Email)?.Value);
            Assert.AreEqual(userInfo.Username, claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            Assert.AreEqual(userInfo.Birthdate.ToString(), claimsIdentity.FindFirst(ClaimTypes.DateOfBirth)?.Value);
        }

        [Test]
        public void GetClaims_ReturnsCorrectClaims()
        {
            // Arrange
            var userInfo = new ExportUserDto
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Birthdate = DateTime.Now.ToString(),
                Role = "admin"
            };
            var userId = "12345";

            // Act
            var claims = _tokenService.GetClaims(userInfo, userId);

            // Assert
            Assert.AreEqual(5, claims.Length);

            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userId));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.Email && c.Value == userInfo.Email));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.Name && c.Value == userInfo.Username));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.DateOfBirth && c.Value == userInfo.Birthdate.ToString()));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.Role && c.Value == userInfo.Role));
        }
    }
}
