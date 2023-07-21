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


        [HttpGet("AllItemTypes"), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AllItemTypes()
        {
            IEnumerable<ItemType> allItemTypes = _menuItemService.GetAllItemTypes();

            return Ok(allItemTypes);
        }


        [HttpGet("AllItemsByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AllItemsByType([FromQuery] string itemType)
        {
            try
            {
                IEnumerable<MenuItemViewModel> menuItems = await _menuItemService.GetAllAsync(itemType);

                return Ok(menuItems);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("ItemDetailsById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> ItemDetails([FromQuery] int itemId)
        {
            try
            {
                if ((await _menuItemService.ItemExistsAsync(itemId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                DetailsMenuItemViewModel item = await _menuItemService.GetItemByIdAsync(itemId);

                return Ok(new
                {
                    item,
                    status = 200
                }); 
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("SimilarProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> SimilarProducts([FromQuery] string itemType, int itemId)
        {
            try
            {
                if ((await _menuItemService.ItemExistsAsync(itemId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                IEnumerable<MenuItemViewModel> items = await _menuItemService
                    .GetFourSimilarItemsByTypeAsync(itemType, itemId);

                return Ok(new
                {
                    items,
                    status = 200,
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
