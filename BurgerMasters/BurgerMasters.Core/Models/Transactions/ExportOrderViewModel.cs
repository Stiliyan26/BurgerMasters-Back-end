using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Transactions
{
    public class ExportOrderViewModel
    {
        public Guid OrderId { get; set; }

        public string Username { get; set; } = null!;

        public string OrderDate { get; set; } = null!;

        public string Address { get; set; } = null!;

        public decimal TotalPrice { get; set; }
    }
}
