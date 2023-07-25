using BurgerMasters.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BurgerMasters.Core.Models.Transactions
{
    public class OrderDetailsMenuItemViewModel
    {
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string ItemType { get; set; } = null!;

        public int PortionSize { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}