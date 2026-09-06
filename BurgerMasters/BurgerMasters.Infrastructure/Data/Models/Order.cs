using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsPending { get; set; } = true;

        public bool IsActive { get; set; } = true;


        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; } 
            = new List<OrderDetail>();

        public decimal TotalPrice { get; set; }
    }
}
