using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Transactions
{
    public class CartInfoViewModel
    {
        [Required]
        public string UserId { get; set; } = null!;

        public int ItemId { get; set; }

        public int Quantity { get; set; }
    }
}
