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

        public async Task CreateMessage(ChatMessage messageInfo)
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
            }
        }

        public async Task<IEnumerable<ExportChatMessage>> GetAllMessages()
        {
            return await _repo.AllReadonly<ReviewMessage>()
                .Select(rm => new ExportChatMessage()
                {
                    Username = rm.ApplicationUser.UserName,
                    Message = rm.Message,
                    SentDate = rm.SentDate.ToString("yyyy-MM-dd HH:mm")
                })
                .ToListAsync();
        }
    }
}
