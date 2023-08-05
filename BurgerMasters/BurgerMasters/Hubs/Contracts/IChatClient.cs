using BurgerMasters.Core.Models.Review;
using BurgerMasters.Infrastructure.Data.Models;

namespace BurgerMasters.Hubs.Contracts
{
    public interface IChatClient
    {
        Task ReceiveMessage(ExportChatMessage message);
    }
}
