using System.Security.Claims;
using BurgerMasters.Controllers;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Core.Services;

namespace BurgerMasters.UnitTests.Controllers
{
    [TestFixture]
    public class CartControllerTests
    {
        private CartController _controller;
        private BurgerMastersDbContext dbContext;
        private IRepository _repo;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase(databaseName: "BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);

            var cartService = new CartService(_repo);

            var menuService = new MenuItemService(_repo);

            _controller = new CartController(cartService, menuService);
            // Simulate a signed-in user with a specific UserId
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "user123") };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [Test]
        public async Task AddItemToCart_WithValidModel_ReturnsOk()
        {
            // Arrange
            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            await _repo.AddAsync(newItemType);

            var item = new MenuItem()
            {
                Id = 1,
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = newItemType.Id,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
            };

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();

            var model = new CartInfoViewModel
            {
                ItemId = 1,
                UserId = "user123",
                Quantity = 2
            };

            // Act
            var result = await _controller.AddItemToCart(model);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
