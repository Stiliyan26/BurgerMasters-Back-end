using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Review;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IRepository _repo;

        public ReviewService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task<ExportChatMessage> CreateMessageAsync(ChatMessage messageInfo)
        {
            ApplicationUser? user = await _repo.AllReadonly<ApplicationUser>()
                .FirstOrDefaultAsync(u => u.Id == messageInfo.UserId);

            if (user != null)
            {
                DateTime sentDate = DateTime
                    .ParseExact(messageInfo.SentDate,
                        "yyyy-MM-dd HH:mm:ss",
                        CultureInfo.InvariantCulture); 

                ReviewMessage newMessage = new ReviewMessage()
                {
                    UserId = messageInfo.UserId,
                    Message = messageInfo.Message,
                    SentDate = sentDate
                };

                await _repo.AddAsync(newMessage);
                await _repo.SaveChangesAsync();



                return await _repo.All<ReviewMessage>()
                    .Where(rm => rm.UserId == newMessage.UserId
                        && rm.Message == newMessage.Message
                        && rm.SentDate == newMessage.SentDate)
                    .Select(rm => new ExportChatMessage()
                    {
                        Id = rm.Id,
                        UserId = rm.UserId,
                        Username = rm.ApplicationUser.UserName,
                        Message = rm.Message,
                        SentDate = rm.SentDate.ToString("yyyy-MM-dd HH:mm:ss")
                    })
                    .FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<IEnumerable<ExportChatMessage>> GetAllMessagesAsync()
        {
            return await _repo.AllReadonly<ReviewMessage>()
                .Where(rm => rm.IsActive)
                .Select(rm => new ExportChatMessage()
                {
                    Id = rm.Id,
                    UserId = rm.UserId,
                    Username = rm.ApplicationUser.UserName,
                    Message = rm.Message,
                    SentDate = rm.SentDate.ToString("yyyy-MM-dd HH:mm:ss")
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveMessageAsync(int messageId, bool isAdmin, string userId)
        {
            ReviewMessage? message = await _repo.All<ReviewMessage>()
                .FirstOrDefaultAsync(rm => 
                    rm.Id == messageId 
                    && rm.IsActive == true
                    && (rm.UserId == userId || isAdmin == true));

            if (message != null)
            {
                message.IsActive = false;

                await _repo.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
