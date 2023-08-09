using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Contracts
{
    public interface ICartService
    {
        Task AddItemToUserCartAsync(CartInfoViewModel model);

        Task<IEnumerable<CartItemInfoViewModel>> GetAllCartItemsByUserIdAsync(string userId);

        Task RemoveItemFromCartByIdAsync(int itemId, string userId);

        Task CleanUpCartAsync(string userId);

        Task<int> GetCartItemsCount(string userId); 
    }
}
