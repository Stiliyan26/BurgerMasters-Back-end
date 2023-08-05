using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Review;
using BurgerMasters.Core.Services;
using BurgerMasters.Hubs;
using BurgerMasters.Hubs.Contracts;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Web.Http.Results;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SentMessage(ChatMessage messageInfo)
        {
            var newMessage = await _reviewService.CreateMessageAsync(messageInfo);

            if (newMessage == null)
            {
                return StatusCode(500,
                         new { 
                             message = ValidationConstants.COULD_NOT_UPDATE,
                             status = 500 
                         });
            }

            await _chatHub.Clients.All.ReceiveMessage(newMessage);

            return Ok(new
            {
                newMessage,
                status = 200
            });
        }

        [HttpGet("AllMessages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> AllMessages()
        {
            var messages = await _reviewService.GetAllMessagesAsync();

            if (messages == null)
            {
                return StatusCode(500,
                        new { message = ValidationConstants.COULD_NOT_UPDATE, status = 500 });
            }

            return Ok(new
            {
                messages,
                status = 200
            });
        }

        [HttpPatch("RemoveMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveMessage(
            [FromBody] int messageId,
            [FromQuery] bool isAdmin, string userId)
        {
            try
            {
                bool isRemoved = await _reviewService.RemoveMessageAsync(messageId, isAdmin, userId);

                if (isRemoved == false)
                {
                    return StatusCode(500,
                        new { message = ValidationConstants.COULD_NOT_UPDATE, status = 500 });
                }

                return Ok(new
                {
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
