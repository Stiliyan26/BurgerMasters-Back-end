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

        [HttpPost("CreateMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]

        public async Task<IActionResult> CreateMenuItem([FromBody]CreateMenuItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                    status = 422
                });
            }
            string userId = GetUserId();

            try
            {
                await _menuItemService.CreateMenuItem(model, userId);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            return Ok(new
            {
                itemType = model.ItemType,
                status = 200
            });;
        }

        [HttpGet("AllItemTypes"), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllItemTypes()
        {
            IEnumerable<ItemType> allItemTypes = _menuItemService.GetAllItemTypes();

            return Ok(allItemTypes);
        }
    }
}
