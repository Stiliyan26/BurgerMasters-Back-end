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
        private static string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase(databaseName: "BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);

            var cartServiceMock = new Mock<CartService>(_repo);
            var menuServiceMock = new Mock<MenuItemService>(_repo);

            _controller = new CartController(cartServiceMock.Object, menuServiceMock.Object);
            // Simulate a signed-in user with a specific UserId
            var claims = new[] { new Claim(ClaimTypes.Name, "user123"),
                new Claim(ClaimTypes.NameIdentifier, userId) };
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
                UserId = userId,
                Quantity = 2
            };

            // Act
            var result = await _controller.AddItemToCart(model);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AllCartItems_ReturnsOk()
        {
            var dummyData = GetApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            var result = await _controller.AllCartItems(userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task RemoveCartItem_ReturnsOk()
        {
            var dummyData = GetApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            var result = await _controller.RemoveCartItem(1, userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CleanUpCart_ReturnsOk()
        {
            var dummyData = GetApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            var result = await _controller.CleanUpCart(userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CartItemsCount_ReturnsOk()
        {
            var dummyData = GetApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            var result = await _controller.CartItemsCount(userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        private static List<ApplicationUserMenuItem> GetApplicationUserMenuItem()
        {
            return new List<ApplicationUserMenuItem>
            {
                new ApplicationUserMenuItem
                {
                    ApplicationUserId = userId,
                    MenuItem = new MenuItem
                    {
                        Id = 1,
                        Name = "Burger",
                        ImageUrl = "burger.jpg",
                        ItemType = new ItemType { Name = "Main Course" },
                        Description = "Bread, Onions",
                        CreatorId = userId,
                        PortionSize = 400,
                        Price = 10.99m,
                    },
                    ItemQuantity = 2
                },
                new ApplicationUserMenuItem
                {
                    ApplicationUserId = userId,
                    MenuItem = new MenuItem
                    {
                        Id = 2,
                        Name = "Fries",
                        ImageUrl = "fries.jpg",
                        ItemType = new ItemType { Name = "Side Dish" },
                        Description = "Bread, Onions",
                        CreatorId = userId,
                        PortionSize = 386,
                        Price = 5.99m,
                    },
                    ItemQuantity = 3
                },
            };
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
