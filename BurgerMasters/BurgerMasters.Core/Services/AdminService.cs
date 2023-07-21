using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMasters.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository _repo;

        public AdminService(IRepository repo)
        {
            _repo = repo;
        }
        public async Task CreateMenuItemAsync(FormMenuItemViewModel createItemModel, string userId)
        {
            ItemType itemType = _repo.AllReadonly<ItemType>()
                .FirstOrDefault(i => i.Name == createItemModel.ItemType);


            if (itemType != null)
            {
                MenuItem newMenuItem = new MenuItem()
                {
                    Name = createItemModel.Name,
                    ImageUrl = createItemModel.ImageUrl,
                    ItemTypeId = itemType.Id,
                    PortionSize = createItemModel.PortionSize,
                    Description = createItemModel.Description,
                    Price = createItemModel.Price,
                    CreaterId = userId
                };

                await _repo.AddAsync(newMenuItem);
                await _repo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAndCreatorAsync(
            string itemType, int itemId, string creatorId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .Where(mi =>
                    mi.IsActive
                    && mi.ItemType.Name == itemType //Should be the same type
                    && mi.Id != itemId // Should not include the current product from details
                    && mi.CreaterId == creatorId // Only by the creator
                 )
                .OrderBy(mi => Guid.NewGuid())
                .Take(4)
                .Select(mi => new MenuItemViewModel
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    ImageUrl = mi.ImageUrl,
                    ItemType = mi.ItemType.Name,
                    PortionSize = mi.PortionSize,
                    Price = mi.Price,
                })
                .ToListAsync();
        }
        public async Task<DetailsMenuItemViewModel> CreatorItemByIdAsync(int itemId, string creatorId)
        {
            return await _repo.AllReadonly<MenuItem>()
                    .Where(mi => mi.IsActive && mi.Id == itemId && mi.CreaterId == creatorId)
                    .Select(mi => new DetailsMenuItemViewModel()
                    {
                        Id = mi.Id,
                        Name = mi.Name,
                        ImageUrl = mi.ImageUrl,
                        ItemType = mi.ItemType.Name,
                        PortionSize = mi.PortionSize,
                        Price = mi.Price,
                        Description = mi.Description,
                        CreatorId = mi.CreaterId
                    })
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetCreatorItemsByTypeAsync(
            string creatorId, string itemType)
        {
            return await _repo.AllReadonly<MenuItem>()
                .Where(mi =>
                    mi.IsActive
                    && mi.CreaterId == creatorId //Only by the creator
                    && mi.ItemType.Name == itemType)
                .Select(mi => new MenuItemViewModel
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    ImageUrl = mi.ImageUrl,
                    ItemType = mi.ItemType.Name,
                    PortionSize = mi.PortionSize,
                    Price = mi.Price,
                })
                .ToListAsync();
        }

        public async Task<bool> ItemExistsByCreatorIdAsync(int itemId, string creatorId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .AnyAsync(mi => mi.IsActive && mi.Id == itemId && mi.CreaterId == creatorId);
        }

        public async Task<ViewEditItemInfoViewModel> GetEditItemInfoByItemIdAsync(int itemId, string creatorId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .Where(mi => mi.IsActive && mi.Id == itemId && mi.CreaterId == creatorId)
                .Select(mi => new ViewEditItemInfoViewModel()
                {
                    Name = mi.Name,
                    ImageUrl = mi.ImageUrl,
                    ItemType = mi.ItemType.Name,
                    PortionSize = mi.PortionSize,
                    Description = mi.Description,
                    Price = mi.Price
                })
                .FirstOrDefaultAsync();
        }

        public async Task EditMenuItemAsync(FormMenuItemViewModel item, int itemId, string creatorId)
        {
            MenuItem menuItem = await _repo.All<MenuItem>()
                .FirstOrDefaultAsync(mi => mi.IsActive && mi.Id == itemId && mi.CreaterId == creatorId);

            if (menuItem != null)
            {
                menuItem.Name = item.Name;
                menuItem.ImageUrl = item.ImageUrl;
                menuItem.PortionSize = item.PortionSize;
                menuItem.Description = item.Description;
                menuItem.Price = item.Price;

                await _repo.SaveChangesAsync();
            }
        }

        public async Task DeleteMenuItemAsync(int itemId, string creatorId)
        {
            MenuItem itemToDelte = await _repo.All<MenuItem>()
                .Where(mi => mi.IsActive && mi.Id == itemId && mi.CreaterId == creatorId)
                .FirstOrDefaultAsync();

            if (itemToDelte != null)
            {
                itemToDelte.IsActive = false;

                await _repo.SaveChangesAsync();
            }
        }
    }
}
