using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Core.Models.Transactions;
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
        Task<IEnumerable<ItemType>> GetAllItemTypesAsync();

        Task<IEnumerable<MenuItemViewModel>> GetAllByItemTypeAsync(string itemType);

        Task<DetailsMenuItemViewModel> GetItemByIdAsync(int id);

        Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAsync(string itemType, int itemId);

        Task<bool> ItemExistsAsync(int itemId);
    }
}
