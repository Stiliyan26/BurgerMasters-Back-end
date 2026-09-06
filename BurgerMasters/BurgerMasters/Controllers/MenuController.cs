using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace BurgerMasters.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IMemoryCache _memoryCache;

        public MenuController(
            IMenuItemService menuItemService,
            IMemoryCache memoryCache)
        {
            _menuItemService = menuItemService;
            _memoryCache = memoryCache;
        }

        [HttpGet("AllItemTypes"), AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ItemType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AllItemTypes()
        {
            if (_memoryCache.TryGetValue<IEnumerable<ItemType>>
                ("AllItemTypes", out var cachedItemTypes))
            {
                return Ok(new
                {
                    allItemTypes = cachedItemTypes,
                    status = 200
                });
            }

            IEnumerable<ItemType> allItemTypes = null;

            await Task.Run(async () =>
            {
                allItemTypes = await _menuItemService.GetAllItemTypesAsync();

                if (allItemTypes != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    };
                    _memoryCache.Set("AllItemTypes", allItemTypes, cacheEntryOptions);
                }
            });


            return Ok(new
            {
                allItemTypes,
                status = 200
            });
        }


        [HttpGet("AllItemsByType")]
        [ProducesResponseType(typeof(IEnumerable<MenuItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AllItemsByType([FromQuery] string itemType)
        {
            return await ProcessActionResult(async () =>
            {
                IEnumerable<MenuItemViewModel> menuItems = await _menuItemService
                    .GetAllByItemTypeAsync(itemType);

                if (menuItems == null)
                {
                    NotFoundHandler();
                }

                return Ok(new
                {
                    menuItems,
                    status = 200
                });
            });
        }

        [HttpGet("ItemDetailsById")]
        [ProducesResponseType(typeof(DetailsMenuItemViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ItemDetails([FromQuery] int itemId)
        {
            return await ProcessActionResult(async () =>
            {
                DetailsMenuItemViewModel item = await _menuItemService.GetItemByIdAsync(itemId);

                return Ok(new
                {
                    item,
                    status = 200
                });
            }, itemId);
        }

        [HttpGet("SimilarProducts")]
        [ProducesResponseType(typeof(IEnumerable<MenuItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> SimilarProducts([FromQuery] string itemType, int itemId)
        {
            return await ProcessActionResult(async () =>
            {
                IEnumerable<MenuItemViewModel> items = await _menuItemService
                  .GetFourSimilarItemsByTypeAsync(itemType, itemId);

                return Ok(new
                {
                    items,
                    status = 200,
                });
            }, itemId);
        }

        [HttpGet("AllMenuItems")]
        [ProducesResponseType(typeof(AllMenuQueryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AllMenuItems([FromQuery] string queryModelString)
        {
            return await ProcessActionResult(async () =>
            {
                var queryModel = JsonConvert
                    .DeserializeObject<AllMenuQueryModel>(queryModelString);

                var result = await _menuItemService.AllMenuItems(
                    queryModel.ItemType,
                    queryModel.SearchTerm,
                    queryModel.Sorting,
                    queryModel.CurrentPage,
                    AllMenuQueryModel.ItemsPerPage);

                queryModel.TotalMenuItemsCount = result.TotalMenuItemsCount;
                queryModel.MenuItems = result.MenuItems;

                return Ok(new
                {
                    queryModel,
                    status = 200
                });
            });
        }

        //Helper methods
        private IActionResult NotFoundHandler()
        {
            return NotFound(new
            {
                errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                status = 404
            });
        }

        //Checks if the item exists
        private async Task<IActionResult> ItemExistsHandler(int itemId)
        {
            if ((await _menuItemService.ItemExistsAsync(itemId)) == false)
            {
                return NotFoundHandler();
            }

            return null;
        }

        //Validation template
        private async Task<IActionResult> ProcessActionResult
            (Func<Task<IActionResult>> action, int itemId = -1)
        {
            try
            {
                if (itemId != -1)
                {
                    IActionResult itemExistsResult = await ItemExistsHandler(itemId);

                    if (itemExistsResult != null)
                    {
                        return itemExistsResult;
                    }
                }

                return action().Result;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
