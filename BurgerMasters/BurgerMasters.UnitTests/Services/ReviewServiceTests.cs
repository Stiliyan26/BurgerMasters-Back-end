using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Core.Models.Review;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BurgerMasters.UnitTests.Services
{
    [TestFixture]
    public class ReviewServiceTests
    {
        private IReviewService _reviewService;
        private IRepository _repo;
        private BurgerMastersDbContext dbContext;
        private static string userId = "146411d7-aee9-42ee-9bdf-618abc2373fd";
        private static string validDateString = "2023-07-31 12:34:22";
        private static string invalidDateString = "2023-07-31 12:34";

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BurgerMastersDbContext>()
                .UseInMemoryDatabase("BurgerMasters_TestDb")
                .Options;

            dbContext = new BurgerMastersDbContext(options);

            _repo = new Repository(dbContext);
            _reviewService = new ReviewService(_repo);
        }

        [Test]
        public async Task CreateMessageAsync_ReturnsExportChatMessage()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            ChatMessage sendingMessage = GetMessage(user.UserName, validDateString);

            var newMessage = await _reviewService.CreateMessageAsync(sendingMessage);

            Assert.That(newMessage, Is.Not.Null);
            Assert.That(newMessage.Message, Is.EqualTo(sendingMessage.Message));
            Assert.That(newMessage.Username, Is.EqualTo(sendingMessage.Username));
            Assert.That(newMessage.SentDate, Is.EqualTo(sendingMessage.SentDate));
            Assert.That(newMessage.UserId, Is.EqualTo(sendingMessage.UserId));
        }

        [Test]

        public async Task CreateMessageAsync_InvalidChatMessageUserId_ReturnsNull()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            ChatMessage sendingMessage = new ChatMessage()
            {
                UserId = "invalidID",
                Username = user.UserName,
                Message = "Food quality is high!",
                SentDate = validDateString,
            };

            var newMessage = await _reviewService.CreateMessageAsync(sendingMessage);


            Assert.That(newMessage, Is.Null);
        }

        [Test]
        public async Task CreateMessageAsync_InvalidChatMessageDate_ReturnsNull()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);
            await _repo.SaveChangesAsync();

            ChatMessage sendingMessage = new ChatMessage()
            {
                UserId = userId,
                Username = user.UserName,
                Message = "Food quality is high!",
                SentDate = invalidDateString,
            };

            var newMessage = await _reviewService.CreateMessageAsync(sendingMessage);


            Assert.That(newMessage, Is.Null);
        }

        [Test]
        public async Task GetAllMessagesAsync_ReturnsProperCount()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);

            await _repo.AddRangeAsync(GetMessages());
            await _repo.SaveChangesAsync();

            var messages = await _reviewService.GetAllMessagesAsync();

            Assert.That(messages.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task RemoveMessageAsync_IsActiveToFalseWhenIsAdminIsTrue_ReturnsTrue()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);

            await _repo.AddRangeAsync(GetMessages());
            await _repo.SaveChangesAsync();

            bool isRemoved = await _reviewService.RemoveMessageAsync(1, true, null);

            var message = await _repo.GetByIdAsync<ReviewMessage>(1);

            Assert.That(isRemoved, Is.True);
            Assert.That(message.IsActive, Is.False);  
        }

        [Test]
        public async Task RemoveMessageAsync_IsActiveToFalseWhenUserIsCreator_ReturnsTrue()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);

            await _repo.AddRangeAsync(GetMessages());
            await _repo.SaveChangesAsync();

            bool isRemoved = await _reviewService.RemoveMessageAsync(1, false, userId);

            var message = await _repo.GetByIdAsync<ReviewMessage>(1);

            Assert.That(isRemoved, Is.True);
            Assert.That(message.IsActive, Is.False);
        }

        [Test]
        public async Task RemoveMessageAsync_IsAdminIsFalseAndUserIsNotCreator_ReturnsFalse()
        {
            ApplicationUser user = GetUser();

            await _repo.AddAsync(user);

            await _repo.AddRangeAsync(GetMessages());
            await _repo.SaveChangesAsync();

            bool isRemoved = await _reviewService.RemoveMessageAsync(1, false, null);

            var message = await _repo.GetByIdAsync<ReviewMessage>(1);

            Assert.That(isRemoved, Is.False);
            Assert.That(message.IsActive, Is.True);
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
