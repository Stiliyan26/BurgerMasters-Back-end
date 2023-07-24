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
        public IEnumerable<ItemType> GetAllItemTypes()
        {
            return _repo.AllReadonly<ItemType>();
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


        public async Task AddItemToUserCartAsync(CartInfoViewModel model)
        {
            ApplicationUserMenuItem? existingTransaction = 
                await _repo.All<ApplicationUserMenuItem>()
                    .FirstOrDefaultAsync(ui =>
                        ui.MenuItem.IsActive
                        && ui.MenuItemId == model.ItemId
                        && ui.ApplicationUserId == model.UserId);

            if (existingTransaction == null)
            {
                ApplicationUserMenuItem newTransaction = new ApplicationUserMenuItem
                {
                    ApplicationUserId = model.UserId,
                    MenuItemId = model.ItemId,
                    ItemQuantity = model.Quantity,
                };

                await _repo.AddAsync(newTransaction);
            } 
            else
            {
                existingTransaction.ItemQuantity += model.Quantity;
            }

            await _repo.SaveChangesAsync();
        }


        public async Task<IEnumerable<CartItemInfoViewModel>> GetAllCartItemsByUserIdAsync(string userId)
        {
            return await _repo.AllReadonly<ApplicationUserMenuItem>()
                .Where(ui =>
                    ui.MenuItem.IsActive
                    && ui.ApplicationUserId == userId)
                .Select(ui => new CartItemInfoViewModel()
                {
                    Id = ui.MenuItem.Id,
                    Name = ui.MenuItem.Name,
                    ImageUrl = ui.MenuItem.ImageUrl,
                    ItemType = ui.MenuItem.ItemType.Name,
                    PortionSize = ui.MenuItem.PortionSize,
                    Price = ui.MenuItem.Price,
                    Quantity = ui.ItemQuantity
                })
                .ToListAsync();
        }

        public async Task RemoveItemFromCartById(int itemId, string userId)
        {
            ApplicationUserMenuItem userItem = await _repo.All<ApplicationUserMenuItem>()
                .FirstOrDefaultAsync(ui => 
                    ui.MenuItem.IsActive
                    && ui.MenuItemId == itemId
                    && ui.ApplicationUserId == userId);

            if (userItem != null)
            {
                _repo.Delete(userItem);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
