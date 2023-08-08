using BurgerMasters.Controllers;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Core.Models.MenuItemModels;

namespace BurgerMasters.UnitTests.Controllers
{
    [TestFixture]
    public class AdminControllerTests
    {
        private AdminController _controller;
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

            var adminServiceMock = new Mock<AdminService>(_repo);

            _controller = new AdminController(adminServiceMock.Object);
            // Simulate a signed-in user with a specific UserId
            var claims = new[] { new Claim(ClaimTypes.Name, "user123"),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Role, "Admin")};
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [Test]
        public async Task CreateMenuItem_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

            await _repo.AddAsync(newItemType);
            await _repo.SaveChangesAsync();

            var formModel = new FormMenuItemViewModel()
            {
                Name = "Triple Cheeseburger",
                ImageUrl = "TrippleCheese.webp",
                ItemType = "Burger",
                PortionSize = 550,
                Description = "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                Price = 22.20m,
            };

            var result = await _controller.CreateMenuItem(formModel, userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task MyItemsByType_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

            await _repo.AddAsync(newItemType);

            var dumyData = GetMenuItems(newItemType.Id);

            await _repo.AddRangeAsync(dumyData);
            await _repo.SaveChangesAsync();

            var result = await _controller.MyItemsByType(userId, newItemType.Name);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task SimilarProductsByCreator_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

            await _repo.AddAsync(newItemType);

            var items = GetMoreMenuItems(newItemType.Id);

            await _repo.AddRangeAsync(items);
            await _repo.SaveChangesAsync();

            var result = await _controller
                .SimilarProductsByCreator(newItemType.Name, newItemType.Id, userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task CreatorItemById_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

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

            var result = await _controller.CreatorItemById(item.Id, userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task EditItemInfo_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

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

            var result = await _controller.EditItemInfo(item.Id, userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task EditMenuItem_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

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

            var formModel = new FormMenuItemViewModel()
            {
                Name = "Triple Cheeseburger",
                ImageUrl = "TrippleCheese.webp",
                PortionSize = 550,
                Description = "Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                Price = 22.20m,
            };

            var result = await _controller.EditMenuItem(formModel, item.Id, userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeleteItem_ReturnsOk()
        {
            ItemType newItemType = GetItemType();

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

            var result = await _controller.DeleteItem(item.Id, userId);

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
        private List<MenuItem> GetMenuItems(int itemTypeId)
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
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Name = "Burger Pie",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Name = "American Cheese Burger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Name = "Triple Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Name = "Smokey Whiskey Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
            };
        }

        private List<MenuItem> GetMoreMenuItems(int itemTypeId)
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = 1,
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 630,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Id = 2,
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Id = 3,
                    Name = "Burger Pie",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Id = 4,
                    Name = "American Cheese Burger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Id = 5,
                    Name = "Triple Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
                },
                new MenuItem()
                {
                    Id = 6,
                    Name = "Smokey Whiskey Cheeseburger",
                    ImageUrl = "Tunder.webp",
                    ItemTypeId = itemTypeId,
                    PortionSize = 380,
                    Description = "Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, \r\nHomemade pickle, Caramelized onions",
                    Price = 27.49m,
                    CreatorId = userId
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
