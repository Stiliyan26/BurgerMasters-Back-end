using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Core.Models.Transactions;
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

        public async Task<IEnumerable<MenuItemViewModel>> GetAllAsync(string itemType)
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

        public async Task<IEnumerable<ItemType>> GetAllItemTypesAsync()
        {
            return await _repo.AllReadonly<ItemType>()
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetFourSimilarItemsByTypeAsync
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

        public async Task<DetailsMenuItemViewModel> GetItemByIdAsync(int id)
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
                CreatorId = menuItem.CreatorId
            };
        }

        public async Task<bool> ItemExistsAsync(int itemId)
        {
            return await _repo.AllReadonly<MenuItem>()
                .AnyAsync(mi => mi.IsActive && mi.Id == itemId);
        }
    }
}
