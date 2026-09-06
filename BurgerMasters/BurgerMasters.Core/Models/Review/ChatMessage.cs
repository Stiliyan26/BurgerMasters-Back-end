using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Review
{
    public class ChatMessage
    {
        public string UserId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Message { get; set; } = null!;

        public string SentDate { get; set; } = null!;
    }
}
