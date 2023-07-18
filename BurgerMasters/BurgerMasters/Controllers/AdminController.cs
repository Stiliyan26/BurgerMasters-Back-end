using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMasters.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IMenuItemService _menuItemService;

        public AdminController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpPost("CreateMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> CreateMenuItem(
            [FromBody] CreateMenuItemViewModel model,
            [FromQuery] string userId
        )
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                    status = 422
                });
            }

            string curretnIdentityId = GetUserId();
            //Checks is the same user sending the request!
            if (userId != curretnIdentityId)
            {
                return Conflict(new
                {
                    errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                    status = 409
                });
            }

            try
            {
                await _menuItemService.CreateMenuItem(model, curretnIdentityId);

                return Ok(new
                {
                    itemType = model.ItemType,
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("MyItemsByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult MyItemsByType([FromQuery] string userId, string itemType)
        {
            try
            {
                string curretnIdentityId = GetUserId();
                //Checks is the same user sending the request!
                if (userId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                IEnumerable<MenuItemViewModel> myItems = _menuItemService
                    .GetMyItemsByType(userId, itemType);

                return Ok(new
                {
                    myItems,
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("SimilarProductsByCreator")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult SimilarProductsByCreator([FromQuery] 
            string itemType,
            int itemId,
            string creatorId)
        {
            try
            {
                IEnumerable<MenuItemViewModel> items =
                    _menuItemService.GetFourSimilarItemsByTypeAndCreator(itemType, itemId, creatorId);

                return Ok(items);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
