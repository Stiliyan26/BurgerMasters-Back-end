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
        public IActionResult AllItemsByType([FromQuery] string itemType)
        {
            try
            {
                IEnumerable<MenuItemViewModel> menuItems = _menuItemService.GetAll(itemType);

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
        public async Task<IActionResult> ItemDetails([FromQuery] int itemId)
        {
            try
            {
                DetailsMenuItemViewModel itemDetails = await _menuItemService.GetItemById(itemId);
                return Ok(itemDetails); 
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
