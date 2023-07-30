using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMasters.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IMenuItemService _menuItemService;

        public MenuController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

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
            (Func<Task<IActionResult>> action, int itemId)
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

        [HttpGet("AllItemTypes"), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AllItemTypes()
        {
            IEnumerable<ItemType> allItemTypes = _menuItemService.GetAllItemTypes();

            if (allItemTypes == null)
            {
                return NotFoundHandler();
            }

            return Ok(allItemTypes);
        }


        [HttpGet("AllItemsByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AllItemsByType([FromQuery] string itemType)
        {
            return await ProcessActionResult(async () =>
            {
                IEnumerable<MenuItemViewModel> menuItems = await _menuItemService.GetAllAsync(itemType);

                if (menuItems == null)
                {
                    NotFoundHandler();
                }

                return Ok(menuItems);
            }, -1);
        }

        [HttpGet("ItemDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
    }
}
