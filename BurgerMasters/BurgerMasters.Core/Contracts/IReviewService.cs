using BurgerMasters.Core.Models.Review;
using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface IReviewService
    {
        Task CreateMessage(ChatMessage messageInfo);

        Task<IEnumerable<ExportChatMessage>> GetAllMessages();
    }
}
