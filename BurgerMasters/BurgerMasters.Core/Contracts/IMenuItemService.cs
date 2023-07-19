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

        Task CreateMenuItem(FormMenuItemViewModel createItemModel, string userId);

        Task<IEnumerable<MenuItemViewModel>> GetAll(string itemType);

        Task<DetailsMenuItemViewModel> GetItemById(int id);

        Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByType(string itemType, int itemId);

        Task<IEnumerable<MenuItemViewModel>> GetMyItemsByType(string userId, string itemType);

        Task<bool> ItemExists(int itemId);

        Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAndCreator(string itemType,
            int itemId, string creatorId);

        Task<bool> ItemExistsByCreatorId(int itemId, string creatorId);

        Task<DetailsMenuItemViewModel> CreatorItemById(int itemId, string creatorId);

        Task<ViewEditItemInfoViewModel> GetEditItemInfoByItemId(int itemId, string creatorId);

        Task EditMenuItem(FormMenuItemViewModel item, int itemId, string creatorId);
    }
}
