using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.UnitTests.Services
{
    [TestFixture]
    public class CartServiceTests
    {
        private ICartService _cartService;
        private IRepository _repo;
        private BurgerMastersDbContext dbContext;
        private static string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase(databaseName: "BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);
            _cartService = new CartService(_repo);
        }

        [Test]
        public async Task AddItemToUserCartAsync_AddsNewItemToCart()
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = "146411d7-aee9-42ee-9bdf-618abc2373fd",
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };

            await _repo.AddAsync(applicationUser);
            await _repo.SaveChangesAsync();

            // Arrange
            var transacion = new CartInfoViewModel()
            {
                UserId = "146411d7-aee9-42ee-9bdf-618abc2373fd",
                ItemId = 1,
                Quantity = 2
            };

            var transacion2 = new CartInfoViewModel()
            {
                UserId = "146411d7-aee9-42ee-9bdf-618abc2373fd",
                ItemId = 2,
                Quantity = 3
            };

            // Act
            await _cartService.AddItemToUserCartAsync(transacion);
            await _cartService.AddItemToUserCartAsync(transacion2);

            // Assert
            var user = await _repo.All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == applicationUser.Id);

            int count = user.CartItems.Count();

            Assert.That(count, Is.EqualTo(2));
        }

        [Test]
        public async Task AddItemToUserCartAsync_IncreasesQuantityAfterExisting()
        {
            // Arrange
            var userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = userId,
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };
            await _repo.AddAsync(applicationUser);


            var dummyData = new ApplicationUserMenuItem
            {
                    ApplicationUserId = userId,
                    MenuItemId = 1,
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
            };

            await _repo.AddAsync(dummyData);
            await _repo.SaveChangesAsync();

            var transacion = new CartInfoViewModel()
            {
                UserId = "146411d7-aee9-42ee-9bdf-618abc2373fd",
                ItemId = 1,
                Quantity = 2
            };

            await _cartService.AddItemToUserCartAsync(transacion);

            var userMenuItem = await _repo.All<ApplicationUserMenuItem>()
                .FirstOrDefaultAsync(u => u.ApplicationUserId == userId
                    && u.MenuItemId == dummyData.MenuItemId);

            Assert.That(4, Is.EqualTo(userMenuItem.ItemQuantity));
        }

        [Test]
        public async Task GetAllCartItemsByUserIdAsync_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            var dummyData = getApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            // Act
            var result = await _cartService.GetAllCartItemsByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(dummyData.Count, result.Count());
        }

        [Test]
        public async Task RemoveItemFromCartByIdAsync_ReturnsCorrectNumberOfItemsAfterRemoval()
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = userId,
                UserName = "Pepsi12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };

            await _repo.AddAsync(applicationUser);
            await _repo.SaveChangesAsync();

            var dummyData = getApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            await _cartService.RemoveItemFromCartByIdAsync(dummyData[0].MenuItemId, userId);

            var user = await _repo.All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            int count = user.CartItems.Count();

            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public async Task CleanUpCartAsync_ReturnsZeroWhenClearingUserCart()
        {
            var userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = userId,
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };

            await _repo.AddAsync(applicationUser);

            var dummyData = new List<ApplicationUserMenuItem>
            {
                new ApplicationUserMenuItem
                {
                    ApplicationUserId = userId,
                    MenuItemId = 1,
                    ItemQuantity = 2
                },
                new ApplicationUserMenuItem
                {
                     ApplicationUserId = userId,
                     MenuItemId = 2,
                     ItemQuantity = 3
                }
            };

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            await _cartService.CleanUpCartAsync(userId);

            var user = await _repo.All<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            int count = user.CartItems.Count();

            Assert.That(count, Is.EqualTo(0));
        }

        [Test]

        public async Task GetCartItemsCount_ReturnsProperCount()
        {
            var dummyData = getApplicationUserMenuItem();

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            int count = await _cartService.GetCartItemsCount(userId);

            Assert.That(count, Is.EqualTo(5));
        }

        private static List<ApplicationUserMenuItem> getApplicationUserMenuItem()
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
