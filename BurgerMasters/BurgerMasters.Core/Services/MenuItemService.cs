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

        public IEnumerable<MenuItemViewModel> GetAll(string itemType)
        {
            return _repo.AllReadonly<MenuItem>()
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
                .ToList();
        }
        public IEnumerable<ItemType> GetAllItemTypes()
        {
            return _repo.AllReadonly<ItemType>();
        }

        public IEnumerable<MenuItemViewModel> GetFourSimilarItemsByType(string itemType, int itemId)
        {
            return _repo.AllReadonly<MenuItem>()
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
                .ToList();
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
                Description = menuItem.Description
            };
        }

        public IEnumerable<MenuItemViewModel> GetMyItems(string userId)
        {
            return _repo.AllReadonly<MenuItem>()
                .Where(mi => mi.IsActive && mi.CreaterId == userId)
                .Select(mi => new MenuItemViewModel
                {
                    Id = mi.Id,
                    Name = mi.Name,
                    ImageUrl = mi.ImageUrl,
                    ItemType = mi.ItemType.Name,
                    PortionSize = mi.PortionSize,
                    Price = mi.Price,
                })
                .ToList();
        }
    }
}
