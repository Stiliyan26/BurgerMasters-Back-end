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
        IEnumerable<ItemType> GetAllItemTypes();

        Task<IEnumerable<MenuItemViewModel>> GetAllAsync(string itemType);

        Task<DetailsMenuItemViewModel> GetItemByIdAsync(int id);

        Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAsync(string itemType, int itemId);

        Task<bool> ItemExistsAsync(int itemId);

        Task AddItemToUserCartAsync(CartInfoViewModel model);

        Task<IEnumerable<CartItemInfoViewModel>> GetAllCartItemsByUserIdAsync(string userId);
    }
}
