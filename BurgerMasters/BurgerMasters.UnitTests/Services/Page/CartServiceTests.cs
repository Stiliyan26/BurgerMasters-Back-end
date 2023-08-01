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

namespace BurgerMasters.UnitTests.Services.Page
{
    [TestFixture]
    public class CartServiceTests
    {
        private ICartService _cartService;
        private Repository _repo;
        private BurgerMastersDbContext dbContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase(databaseName: "BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            _repo = new Repository(dbContext);
            _cartService = new CartService(_repo);
        }

        [Test]
        public async Task AddItemToUserCartAsync_AddsNewItemToCart()
        {
            //Arrange
            var transacion = new CartInfoViewModel()
            {
                UserId = "146411d7-aee9-42ee-9bdf-618abc2373fd",
                ItemId = 1,
                Quantity = 2
            };

            var transacion2 = new CartInfoViewModel()
            {
                UserId = "4c7cfb21-ddb4-4f1a-9486-0c05d7b99bb7",
                ItemId = 2,
                Quantity = 3
            };

            //Act
            await _cartService.AddItemToUserCartAsync(transacion);
            await _cartService.AddItemToUserCartAsync(transacion2);

            //Assert
            var allCartItems = await dbContext.Set<ApplicationUserMenuItem>()
                .ToListAsync();


            Assert.That(allCartItems.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllCartItemsByUserIdAsync_ReturnsCorrectNumberOfItems()
        {
            // Arrange
            var userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            var dummyData = new List<ApplicationUserMenuItem>
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
            var userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";

            var dummyData = new List<ApplicationUserMenuItem>
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

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            await _cartService.RemoveItemFromCartByIdAsync(1, userId);

            var item = await _repo.AllReadonly<ApplicationUserMenuItem>()
                .FirstOrDefaultAsync(ui =>
                    ui.MenuItemId == 1
                    && ui.ApplicationUserId == userId);

            Assert.That(item, Is.EqualTo(null));  
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
            await _repo.SaveChangesAsync();

            var dummyData = new List<ApplicationUserMenuItem>
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

            await _repo.AddRangeAsync(dummyData);
            await _repo.SaveChangesAsync();

            await _cartService.CleanUpCartAsync(userId);

            var user = await _repo.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == userId);

            int count = user.CartItems.Count();

            Assert.That(count, Is.EqualTo(0));
        }


        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
