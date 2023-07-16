using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface IMenuItemService
    {
        IEnumerable<ItemType> GetAllItemTypes();

        Task CreateMenuItem(CreateMenuItemViewModel createItemModel, string userId);

        IEnumerable<MenuItemViewModel> GetAll(string itemType);

        Task<DetailsMenuItemViewModel> GetItemById(int id);
    }
}
