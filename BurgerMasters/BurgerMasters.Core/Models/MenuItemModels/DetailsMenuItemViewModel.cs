using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItemModels
{
    public class DetailsMenuItemViewModel : MenuItemViewModel
    {
        public string Description { get; set; } = null!;
    }
}
