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
using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Core.Models.Transactions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace BurgerMasters.UnitTests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderService _orderService;
        private IRepository _repo;
        private BurgerMastersDbContext dbContext;
        private static string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";
        private static string dateString = "2023-07-31 12:34";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase("BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);
            _orderService = new OrderService(_repo);
        }

        [Test]
        public async Task CreateOrderAsync_ChecksIfOrderIsCreatedWithValidDate()
        {
            var menuItems = GetOrderMenuItemViewModels();

            OrderViewModel model = new OrderViewModel()
            {
                OrderDate = dateString,
                UserId = userId,
                MenuItems = menuItems,
                OrderPrice = 100
            };

            await _orderService.CreateOrderAsync(model);

            var existingOrder = await _repo.AllReadonly<Order>()
                .FirstOrDefaultAsync(o => o.UserId == userId);

            Assert.That(existingOrder, Is.Not.Null);
        }

        [Test]
        public async Task CreateOrderAsync_ReturnsNullIfOrderDateIsInvalid()
        {
            var menuItems = new List<OrderMenuItemViewModel>()
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

            OrderViewModel model = new OrderViewModel()
            {
                OrderDate = "12/12/333/22",
                UserId = userId,
                MenuItems = menuItems,
                OrderPrice = 100
            };

            await _orderService.CreateOrderAsync(model);

            var existingOrder = await _repo.AllReadonly<Order>()
                .FirstOrDefaultAsync(o => o.UserId == userId);

            Assert.That(existingOrder, Is.Null);
        }

        [Test]
        public async Task GetAllOrdersByStatus_ReturnsCountOfAllPendingOrders()
        {
            ApplicationUser applicationUser = GetUser();

            await _repo.AddAsync(applicationUser);

            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var ordersByStatus = await _orderService.GetAllOrdersByStatus(true);

            Assert.That(ordersByStatus.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetAllOrdersByStatus_ReturnsCountOfAllAcceptedOrders()
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = userId,
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };

            await _repo.AddAsync(applicationUser);

            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var ordersByStatus = await _orderService.GetAllOrdersByStatus(false);

            Assert.That(ordersByStatus.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetOrderByIdAsync_ReturnsOrderById()
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = userId,
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };

            await _repo.AddAsync(applicationUser);

            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var order = await _orderService.GetOrderDetailsByIdAsync(orders[0].Id);

            Assert.That(order, Is.Not.Null);
        }

        [Test]
        public async Task GetOrderByIdAsync_CheckIfOrderIsNull()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);

            var orderById = await _orderService.GetOrderByIdAsync(orders[0].Id);

            Assert.That(orderById, Is.Not.Null);
        }

        [Test]
        public async Task AcceptOrderAsync_ChecksIfOrderIsAccepted()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            await _orderService.AcceptOrderAsync(orders[0].Id);

            var acceptedOrder = await _repo.GetByIdAsync<Order>(orders[0].Id);

            Assert.That(acceptedOrder.IsPending, Is.EqualTo(false));
        }

        [Test]
        public async Task UnacceptOrderAsync_ChecksIfOrderIsUnAccepted()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            await _orderService.UnacceptOrderAsync(orders[1].Id);

            var acceptedOrder = await _repo.GetByIdAsync<Order>(orders[1].Id);

            Assert.That(acceptedOrder.IsPending, Is.EqualTo(true));
        }

        [Test]
        public async Task DeclineOrderAsync_ChecksIfOrderIsDeclined()
        {
            var orders = GetOrders();

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            await _orderService.DeclineOrderAsync(orders[0].Id);

            var acceptedOrder = await _repo.GetByIdAsync<Order>(orders[0].Id);

            Assert.That(acceptedOrder.IsActive, Is.EqualTo(false));
        }

        [Test]
        public async Task GetAllOrdersByUserId_ReturnsCountOfOrdersByUser()
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                Id = userId,
                UserName = "Test12",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };

            await _repo.AddAsync(applicationUser);

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
                    UserId = userId,
                    IsPending = true,
                    OrderDate = DateTime.Now,
                    TotalPrice = 100,
                    OrderDetails = orderDetails
                },
                new Order()
                {
                    UserId = userId,
                    IsPending = false,
                    OrderDate = DateTime.Now,
                    TotalPrice = 120,
                    OrderDetails = orderDetails
                },
                new Order()
                {
                    UserId = "1233",
                    IsPending = false,
                    OrderDate = DateTime.Now,
                    TotalPrice = 160,
                    OrderDetails = orderDetails
                }
            };

            await _repo.AddRangeAsync(orders);
            await _repo.SaveChangesAsync();

            var ordersByUserId = await _orderService.GetAllOrdersByUserId(userId);

            Assert.That(ordersByUserId.Count(), Is.EqualTo(2));
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
                    UserId = userId,
                    IsPending = false,
                    OrderDate = DateTime.Now,
                    TotalPrice = 120,
                    OrderDetails = orderDetails
                }
            };
            return orders;
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

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }
    }
}
