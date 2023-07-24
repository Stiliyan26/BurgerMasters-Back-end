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
    public class OrderDetailViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string ItemType { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        public int PortionSize { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
