using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository _repo;

        public MenuItemService(IRepository repo)
        {
            _repo = repo;
        }

        public async Task CreateMenuItem(FormMenuItemViewModel createItemModel, string userId)
        {
            ItemType itemType = GetAllItemTypes()
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

        public async Task<IEnumerable<MenuItemViewModel>> GetAll(string itemType)
        {
            return await _repo.AllReadonly<MenuItem>()
                .Where(mi => mi.IsActive && mi.ItemType.Name == itemType)
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
        public IEnumerable<ItemType> GetAllItemTypes()
        {
            return _repo.AllReadonly<ItemType>();
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByType
            (string itemType, int itemId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .Where(mi =>
                    mi.IsActive
                    && mi.ItemType.Name == itemType //Should be the same type
                    && mi.Id != itemId // Should not include the current product from details
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

        public async Task<DetailsMenuItemViewModel> GetItemById(int id)
        {
            MenuItem menuItem = await _repo
                .GetByIdIncludeTypesAsync<MenuItem>(id, m => m.ItemType);

            return new DetailsMenuItemViewModel()
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                ImageUrl = menuItem.ImageUrl,
                ItemType = menuItem.ItemType.Name,
                PortionSize = menuItem.PortionSize,
                Price = menuItem.Price,
                Description = menuItem.Description,
                CreatorId = menuItem.CreaterId
            };
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetMyItemsByType(string userId, string itemType)
        {
            return await _repo.AllReadonly<MenuItem>()
                .Where(mi =>
                    mi.IsActive
                    && mi.CreaterId == userId //Only by the creator
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

        public async Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAndCreator(
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

        public async Task<bool> ItemExists(int itemId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .AnyAsync(mi => mi.IsActive && mi.Id == itemId);
        }

        public async Task<DetailsMenuItemViewModel> CreatorItemById(int itemId, string creatorId)
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

        public async Task<bool> ItemExistsByCreatorId(int itemId, string creatorId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .AnyAsync(mi => mi.IsActive && mi.Id == itemId && mi.CreaterId == creatorId);
        }

        public async Task<ViewEditItemInfoViewModel> GetEditItemInfoByItemId(int itemId, string creatorId)
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

        public async Task EditMenuItem(FormMenuItemViewModel item, int itemId, string creatorId)
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
    }
}
