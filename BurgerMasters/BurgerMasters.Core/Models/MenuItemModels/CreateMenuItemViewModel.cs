using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItemModels
{
    public class CreateMenuItemViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[A-Z][a-z]{2,}(?: [A-Za-z][a-z]{2,})*$")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public string ItemType { get; set; } = null!;

        [Required]
        [Range(200, 800)]
        public int PortionSize { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "2.00", "30.00")]
        public decimal Price { get; set; } 
    }
}
