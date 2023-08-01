using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerMasters.Core.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository _repo;

        public CartService(IRepository repo)
        {
            _repo = repo;
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

        public async Task RemoveItemFromCartByIdAsync(int itemId, string userId)
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

        public async Task CleanUpCartAsync(string userId)
        {
            var allCartItems = await _repo.All<ApplicationUserMenuItem>()
                    .Where(ui => ui.ApplicationUserId == userId)
                    .ToListAsync();

            if (allCartItems.Any())
            {
                _repo.DeleteRange(allCartItems);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
