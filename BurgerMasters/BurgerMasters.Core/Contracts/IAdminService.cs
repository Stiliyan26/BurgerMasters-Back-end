using BurgerMasters.Core.Models.MenuItemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface IAdminService
    {
        Task CreateMenuItemAsync(FormMenuItemViewModel createItemModel, string userId);

        Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAndCreatorAsync(string itemType,
            int itemId, string creatorId);

        Task<bool> ItemExistsByCreatorIdAsync(int itemId, string creatorId);

        Task<DetailsMenuItemViewModel> CreatorItemByIdAsync(int itemId, string creatorId);

        Task<IEnumerable<MenuItemViewModel>> GetCreatorItemsByTypeAsync(string creatorId, string itemType);

        Task<ViewEditItemInfoViewModel> GetEditItemInfoByItemIdAsync(int itemId, string creatorId);

        Task EditMenuItemAsync(FormMenuItemViewModel item, int itemId, string creatorId);

        Task DeleteMenuItemAsync(int itemId, string creatorId);
    }
}
