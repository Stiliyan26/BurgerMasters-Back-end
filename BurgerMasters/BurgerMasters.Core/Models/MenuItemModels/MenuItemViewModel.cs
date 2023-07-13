using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItemModels
{
    public class MenuItemViewModel
    {
        public int Id { get; set; } 

        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string ItemType { get; set; } = null!;

        public int PortionSize { get; set; }

        public decimal Price { get; set; }
    }
}
