using BurgerMasters.Core.Models.MenuItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItem
{
    public class MenuItemsQueryModel
    {
        public int TotalMenuItemsCount { get; set; }

        public IEnumerable<MenuItemViewModel> MenuItems { get; set; } =
                new List<MenuItemViewModel>();
    }
}
