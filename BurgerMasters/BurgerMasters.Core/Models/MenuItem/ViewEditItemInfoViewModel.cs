using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItemModels
{
    public class ViewEditItemInfoViewModel
    {
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string ItemType { get; set; } = null!;

        public int PortionSize { get; set; }

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
