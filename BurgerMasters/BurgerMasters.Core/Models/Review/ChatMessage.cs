using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Review
{
    public class ChatMessage
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Message { get; set; }

        public string SentDate { get; set; }
    }
}
