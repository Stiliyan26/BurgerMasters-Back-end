using BurgerMasters.Core.Contracts;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Controllers;
using BurgerMasters.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using BurgerMasters.Infrastructure.Data.Models;
using System.Web.Http.Results;

namespace BurgerMasters.UnitTests.Controllers
{
    [TestFixture]
    public class MenuControllerTests
    {
        private MenuController _controller;
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

            var menuItemServiceMock = new Mock<MenuItemService>(_repo);
            var _memoryCache = new MemoryCache(new MemoryCacheOptions());

            _controller = new MenuController(menuItemServiceMock.Object, _memoryCache);
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
        public async Task AllItemTypes_ReturnsOk()
        {
            await _repo.AddRangeAsync(GetItems());
            await _repo.SaveChangesAsync();

            var result = await _controller.AllItemTypes();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task GetAllAsync_ReturnsOk()
        {
            ItemType newItemType = new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };

            var dumyData = GetMenuItems(newItemType.Id);

            await _repo.AddAsync(newItemType);
            await _repo.AddRangeAsync(dumyData);
            await _repo.SaveChangesAsync();

            var result = await _controller.AllItemsByType(newItemType.Name);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task ItemDetails_ReturnsOk()
        {
            ItemType newItemType = GetItemType();
            await _repo.AddAsync(newItemType);

            var item = GetMenuItem(newItemType.Id);

            await _repo.AddAsync(item);
            await _repo.SaveChangesAsync();

            var result = await _controller.ItemDetails(item.Id);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }


        [Test]
        public async Task SimilarProducts_ReturnsOk()
        {
            ItemType newItemType = GetItemType();
            await _repo.AddAsync(newItemType);

            var items = GetManyMenuItems(newItemType.Id);

            await _repo.AddRangeAsync(items);
            await _repo.SaveChangesAsync();

            var result = await _controller.SimilarProducts(items[0].Name, items[0].Id);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        private ItemType GetItemType()
        {
            return new ItemType()
            {
                Id = 1,
                Name = "Burger"
            };
        }
        private static List<ItemType> GetItems()
        {
            return new List<ItemType>()
            {
                new ItemType
                {
                    Id = 1,
                    Name = "Burger"
                },
                new ItemType
                {
                    Id = 2,
                    Name = "Fries"
                },
            };
        }

        private static MenuItem GetMenuItem(int itemTypeId)
        {
            return new MenuItem()
            {
                Name = "RUSTY SAVAGE",
                ImageUrl = "RustySavage.webp",
                ItemTypeId = itemTypeId,
                PortionSize = 630,
                Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                Price = 27.49m,
                CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
            };
        }

        private static List<MenuItem> GetMenuItems(int itemTypeId)
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
            };
        }

        private static List<MenuItem> GetManyMenuItems(int newItemTypeId)
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = 1,
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Id = 2,
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Id = 3,
                    Name = "Burger Pie",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Id = 4,
                    Name = "American Cheese Burger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Id = 5,
                    Name = "Triple Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
                },
                new MenuItem()
                {
                    Id = 6,
                    Name = "Smokey Whiskey Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = newItemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = "146411d7-aee9-42ee-9bdf-618abc2373fd"
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
