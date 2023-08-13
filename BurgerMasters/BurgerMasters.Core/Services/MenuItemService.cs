using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        private readonly IMapper _mapper;

        public MenuItemService(
            IRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<MenuItemsQueryModel> AllMenuItems(
            string itemTpye,
            string searchTerm,
            MenuSorting sorting,
            int currentPage,
            int menuItemsPerPage)
        {
            var result = new MenuItemsQueryModel();

            var menuItems = _repo.AllReadonly<MenuItem>()
                .Where(mi => mi.IsActive && mi.ItemType.Name == itemTpye);

            if (string.IsNullOrEmpty(searchTerm) == false)
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                menuItems = menuItems
                    .Where(mi => EF.Functions.Like(mi.Name.ToLower(), searchTerm));
            }

            menuItems = sorting switch
            {
                MenuSorting.Name => menuItems
                    .OrderBy(mi => mi.Name),

                MenuSorting.PriceAscending => menuItems
                    .OrderBy(mi => mi.Price),

                MenuSorting.PriceDescending => menuItems
                    .OrderByDescending(mi => mi.Price),

                MenuSorting.PortionSizeDescending => menuItems
                    .OrderByDescending(mi => mi.PortionSize),

                MenuSorting.Default => menuItems
                    .OrderBy(mi => mi.Id),

            _ => menuItems
                    .OrderBy(mi => mi.Id)
            };

            result.MenuItems = await menuItems
                .Skip((currentPage - 1) * menuItemsPerPage)
                .Take(menuItemsPerPage)
                .ProjectTo<MenuItemViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            int menuItemsCount = await menuItems.CountAsync();

            result.TotalMenuItemsCount = menuItemsCount;

            return result;
        }

        public async Task<IEnumerable<MenuItemViewModel>> GetAllByItemTypeAsync(string itemType)
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
