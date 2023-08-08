using BurgerMasters.Core.Models.Review;
using BurgerMasters.Infrastructure.Data.Models;

namespace BurgerMasters.Core.Contracts
{
    public interface IChatClient
    {
        Task ReceiveMessage(ExportChatMessage message);

        Task RemoveMessage(int messageId);
    }
}
