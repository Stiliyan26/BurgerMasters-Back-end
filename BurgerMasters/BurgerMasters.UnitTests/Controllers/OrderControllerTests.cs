using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Infrastructure.Data.Models;

namespace BurgerMasters.UnitTests.Controllers
{
    [TestFixture]
    public class OrderControllerTests
    {
        private OrderController _controller;
        private BurgerMastersDbContext dbContext;
        private IRepository _repo;
        private static string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";
        private static string dateString = "2023-07-31 12:34";


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase(databaseName: "BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);

            var orderServiceMock = new Mock<OrderService>(_repo);
            var menuServiceMock = new Mock<MenuItemService>(_repo);

            _controller = new OrderController(orderServiceMock.Object);
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
        public async Task SentOrder_ReturnsOk()
        {
            var menuItems = GetOrderMenuItemViewModels();

            OrderViewModel model = new OrderViewModel()
            {
                OrderDate = dateString,
                UserId = userId,
                MenuItems = menuItems,
                OrderPrice = 100
            };

            var result = await _controller.SentOrder(model);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AllOrdersByStatus_ReturnsOk()
        {
            ApplicationUser applicationUser = GetUser();

            await _repo.AddAsync(applicationUser);
            await _repo.AddRangeAsync(GetOrders());

            var result = await _controller.AllOrdersByStatus(userId, true);
            var result2 = await _controller.AllOrdersByStatus(userId, false);


            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));

            Assert.That(result2, Is.TypeOf<OkObjectResult>());
            var okResult2 = (OkObjectResult)result2;
            Assert.That(okResult2.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task OrderById_ReturnsOk()
        {
            ApplicationUser applicationUser = GetUser();

            await _repo.AddAsync(applicationUser);

            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var ords = await _repo.AllReadonly<Order>()
                .ToListAsync();

            var result = await _controller
                .OrderById(userId, orders[0].Id);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AcceptOrder_ReturnsOk()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var result = await _controller.AcceptOrder(userId, orders[0].Id);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task DeclineOrder_ReturnsOk()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var result = await _controller.DeclineOrder(userId, orders[0].Id);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task UnacceptOrder_ReturnsOk()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var result = await _controller.UnacceptOrder(userId, orders[0].Id);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AllOfMyOrders_ReturnsOk()
        {
            ApplicationUser applicationUser = GetUser();

            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var result = await _controller.AllOfMyOrders(userId);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }


        private static List<OrderMenuItemViewModel> GetOrderMenuItemViewModels()
        {
            return new List<OrderMenuItemViewModel>()
            {
                new OrderMenuItemViewModel()
                {
                    MenuItemId = 1,
                    Quantity = 1,
                },
                new OrderMenuItemViewModel()
                {
                    MenuItemId = 2,
                    Quantity = 3,
                },
            };
        }

        private static ApplicationUser GetUser()
        {
            return new ApplicationUser()
            {
                Id = userId,
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };
        }

        private List<Order> GetOrders()
        {
            var orderDetails = new List<OrderDetail>()
            {
                new OrderDetail()
                {
                    MenuItemId = 1,
                    Quantity = 1,
                },
                new OrderDetail()
                {
                    MenuItemId = 2,
                    Quantity = 4,
                }
            };

            var orders = new List<Order>()
            {
                new Order()
                {
                    Id = new Guid("3714ee0a-d1d4-4655-9692-d7e3e4a78a54"),
                    UserId = userId,
                    IsPending = true,
                    OrderDate = DateTime.Now,
                    TotalPrice = 100,
                    OrderDetails = orderDetails
                },
                new Order()
                {
                    Id = new Guid("2f3efafe-2750-4c2e-a815-e2dbc118e962"),
                    UserId = userId,
                    IsPending = false,
                    OrderDate = DateTime.Now,
                    TotalPrice = 120,
                    OrderDetails = orderDetails
                }
            };

            return orders;
        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
