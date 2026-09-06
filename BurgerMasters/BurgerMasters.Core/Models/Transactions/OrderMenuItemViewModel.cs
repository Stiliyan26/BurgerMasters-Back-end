using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Transactions
{
    public class OrderMenuItemViewModel
    {
        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
