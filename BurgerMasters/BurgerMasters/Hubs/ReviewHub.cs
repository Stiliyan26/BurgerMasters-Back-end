﻿using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Review;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.SignalR;

namespace BurgerMasters.Hubs
{
    public class ReviewHub : Hub<IChatClient>
    {
        /*public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.ReceiveMessage(message);
        }*/
    }
}
