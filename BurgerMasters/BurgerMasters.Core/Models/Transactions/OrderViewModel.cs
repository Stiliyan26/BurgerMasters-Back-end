using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Transactions
{
    public class OrderViewModel
    {
        [Required]
        [RegularExpression(@"^(\d{4})-(\d{2})-(\d{2}) (\d{2}):(\d{2})$")]
        public string OrderDate { get; set; } = null!;

        [Required]
        public string UserId { get; set; }

        public ICollection<OrderMenuItemViewModel> MenuItems { get; set; }
            = new List<OrderMenuItemViewModel>();

        [Required]
        public decimal OrderPrice { get; set; }
    }
}
