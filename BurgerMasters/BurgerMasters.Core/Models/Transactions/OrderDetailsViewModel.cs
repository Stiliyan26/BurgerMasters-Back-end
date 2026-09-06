using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Transactions
{
    public class OrderDetailsViewModel
    {
        public Guid OrderId { get; set; }

        public string Username { get; set; } = null!;

        public string OrderDate { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal TotalPrice { get; set; }

        public List<OrderDetailsMenuItemViewModel> MenuItems = 
            new List<OrderDetailsMenuItemViewModel>();
    }
}
