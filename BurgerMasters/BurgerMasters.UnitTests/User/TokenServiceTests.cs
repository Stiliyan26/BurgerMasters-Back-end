using BurgerMasters.Core.Models;
using BurgerMasters.Core.Models.Auth;
using BurgerMasters.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                Id = "1234",
                Username = "testuser",
                Email = "testuser@example.com",
                Address = "Orehova gora number 20",
                Birthdate = DateTime.Now.ToString()
            };
            

            _configurationMock.SetupGet(c => c["Jwt:Key"]).Returns("mysecretkey123dasdad3e23dadasda");
            _configurationMock.SetupGet(c => c["Jwt:Issuer"]).Returns("myissuer");
            _configurationMock.SetupGet(c => c["Jwt:Audience"]).Returns("myaudience");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecretkey123dasdad3e23dadasda"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = _tokenService.GenerateToken(userInfo);

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

            Assert.AreEqual(userInfo.Id, claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            Assert.AreEqual(userInfo.Email, claimsIdentity.FindFirst(ClaimTypes.Email)?.Value);
            Assert.AreEqual(userInfo.Address, claimsIdentity.FindFirst(ClaimTypes.StreetAddress)?.Value);
            Assert.AreEqual(userInfo.Username, claimsIdentity.FindFirst(ClaimTypes.Name)?.Value);
            Assert.AreEqual(userInfo.Birthdate.ToString(), claimsIdentity.FindFirst(ClaimTypes.DateOfBirth)?.Value);
        }

        [Test]
        public void GetClaims_ReturnsCorrectClaims()
        {
            // Arrange
            var userInfo = new ExportUserDto
            {
                Id = "12345",
                Username = "testuser",
                Email = "testuser@example.com",
                Address = "Orehova gora number 20",
                Birthdate = DateTime.Now.ToString(),
                Role = "admin"
            };
            // Act
            var claims = _tokenService.GetClaims(userInfo);

            // Assert
            Assert.AreEqual(6, claims.Length);

            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.NameIdentifier && c.Value == userInfo.Id));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.Email && c.Value == userInfo.Email));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.StreetAddress && c.Value == userInfo.Address));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.Name && c.Value == userInfo.Username));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.DateOfBirth && c.Value == userInfo.Birthdate.ToString()));
            Assert.IsTrue(claims.Any(c => c.Type == ClaimTypes.Role && c.Value == userInfo.Role));
        }
    }
}