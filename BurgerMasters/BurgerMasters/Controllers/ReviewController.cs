using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Review;
using BurgerMasters.Core.Services;
using BurgerMasters.Hubs;
using BurgerMasters.Hubs.Contracts;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BurgerMasters.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IHubContext<ReviewHub, IChatClient> _chatHub;
        private readonly IReviewService _reviewService;

        public ReviewController(
            IHubContext<ReviewHub, IChatClient> chatHub,
            IReviewService reviewService)
        {
            _chatHub = chatHub;
            _reviewService = reviewService;
        }

        [HttpPost("SentMessage")]
        public async Task Post(ChatMessage messageInfo)
        {
            await _reviewService.CreateMessage(messageInfo);

            await _chatHub.Clients.All.ReceiveMessage(messageInfo);
        }

        [HttpGet("AllMessages")]
        public async Task<IActionResult> AllMessages()
        {
            var messages = await _reviewService.GetAllMessages();

            return Ok(new
            {
                messages,
                status = 200
            });
        }
    }
}
