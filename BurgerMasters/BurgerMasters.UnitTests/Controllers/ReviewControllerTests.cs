using BurgerMasters.Controllers;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Hubs;
using Microsoft.AspNetCore.SignalR;
using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Core.Models.Review;
using BurgerMasters.Core.Contracts;

namespace BurgerMasters.UnitTests.Controllers
{
    [TestFixture]
    public class ReviewControllerTests
    {
        private ReviewController _controller;
        private BurgerMastersDbContext dbContext;
        private IRepository _repo;
        private static string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";
        private static string dateString = "2023-07-31 12:34:22";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase(databaseName: "BurgerMasters_TestDb")
                .Options;
            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);

            var chatClientMock = new Mock<IChatClient>();

            chatClientMock.Setup(client => client
                .ReceiveMessage(It.IsAny<ExportChatMessage>())).Verifiable();

            var hubContextMock = new Mock<IHubContext<ReviewHub, IChatClient>>();
            hubContextMock.Setup(hubContext => hubContext.Clients.All).Returns(chatClientMock.Object);


            var reviewServicerMock = new Mock<ReviewService>(_repo);

            _controller = new ReviewController(hubContextMock.Object, reviewServicerMock.Object);
            // Simulate a signed-in user with a specific UserId
            var claims = new[] { new Claim(ClaimTypes.Name, "user123"),
                new Claim(ClaimTypes.NameIdentifier, userId)};
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var principal = new ClaimsPrincipal(identity);
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = principal }
            };
        }

        [Test]
        public async Task SentMessage_ReturnsOk()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            ChatMessage sendingMessage = new ChatMessage()
            {
                UserId = userId,
                Username = user.UserName,
                Message = "Food quality is high!",
                SentDate = dateString,
            };

            var result = await _controller.SentMessage(sendingMessage);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task AllMessages_ReturnsOk()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);

            await _repo.AddRangeAsync(GetMessages());
            await _repo.SaveChangesAsync();

            var result = await _controller.AllMessages();

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task RemoveMessage_ReturnsOk()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);

            await _repo.AddRangeAsync(GetMessages());
            await _repo.SaveChangesAsync();

            var result = await _controller.RemoveMessage(1, true, null);

            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        private static List<ReviewMessage> GetMessages()
        {
            ReviewMessage message1 = new ReviewMessage()
            {
                Id = 1,
                UserId = userId,
                Message = "Food quality is high!",
                SentDate = new DateTime(2023, 2, 12, 13, 12, 13),
            };

            ReviewMessage message2 = new ReviewMessage()
            {
                Id = 2,
                UserId = userId,
                Message = "Resturant is awsome!",
                SentDate = new DateTime(2023, 3, 17, 11, 6, 19),
            };

            var messages = new List<ReviewMessage>
            {
                message1,
                message2
            };

            return messages;
        }

        private static ApplicationUser GetUser()
        {
            return new ApplicationUser()
            {
                Id = userId,
                UserName = "Kiril",
                Birthdate = DateTime.Now,
                Address = "Orehova gora 10"
            };
        }

        private static ChatMessage GetMessage(string username, string dateString)
        {
            return new ChatMessage()
            {
                UserId = userId,
                Username = username,
                Message = "Food quality is high!",
                SentDate = dateString,
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
