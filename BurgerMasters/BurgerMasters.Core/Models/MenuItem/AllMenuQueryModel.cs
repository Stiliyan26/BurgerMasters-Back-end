using BurgerMasters.Core.Models.MenuItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItem
{
    public class AllMenuQueryModel
    {
        public const int ItemsPerPage = 6;

        public string ItemType { get; set; } = null!;

        public string? SearchTerm { get; set; } = null!;

        public MenuSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalMenuItemsCount { get; set; }


        public IEnumerable<MenuItemViewModel> MenuItems 
            = Enumerable.Empty<MenuItemViewModel>();
    }
}
