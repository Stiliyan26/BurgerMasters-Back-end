using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Common.Repository;
using BurgerMasters.Infrastructure.Data.Models;
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

        public async Task CreateMenuItem(CreateMenuItemViewModel createItemModel, string userId)
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

        public IEnumerable<ItemType> GetAllItemTypes()
        {
            return _repo.AllReadonly<ItemType>();
        }
    }
}
