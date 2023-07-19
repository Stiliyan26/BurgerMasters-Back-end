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
            [FromBody] FormMenuItemViewModel model,
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

        public async Task<IActionResult> MyItemsByType([FromQuery] string userId, string itemType)
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

                IEnumerable<MenuItemViewModel> myItems = await _menuItemService
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> SimilarProductsByCreator([FromQuery]
            string itemType,
            int itemId,
            string creatorId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (creatorId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                if ((await _menuItemService.ItemExists(itemId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                IEnumerable<MenuItemViewModel> items =
                    await _menuItemService.GetFourSimilarItemsByTypeAndCreator(itemType, itemId, creatorId);

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

        [HttpGet("CreatorItemById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> CreatorItemById([FromQuery] int itemId, string creatorId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (creatorId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                if ((await _menuItemService.ItemExistsByCreatorId(itemId, creatorId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                DetailsMenuItemViewModel item = await _menuItemService.CreatorItemById(itemId, creatorId);

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

        [HttpGet("EditItemInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditItemInfo([FromQuery] int itemId, string creatorId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (creatorId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                if ((await _menuItemService.ItemExistsByCreatorId(itemId, creatorId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                ViewEditItemInfoViewModel item = await _menuItemService
                    .GetEditItemInfoByItemId(itemId, creatorId);

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

        [HttpPut("EditMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> EditMenuItem([FromBody] FormMenuItemViewModel model,
            [FromQuery] int itemId, string creatorId)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                    status = 422
                });
            }

            try
            {
                string curretnIdentityId = GetUserId();
                //Checks is the same user sending the request!
                if (creatorId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                if ((await _menuItemService.ItemExistsByCreatorId(itemId, creatorId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }


                await _menuItemService.EditMenuItem(model, itemId, creatorId);

                return Ok(new
                {
                    itemId,
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpDelete("DeleteItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteItem([FromQuery]int itemId, string creatorId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (creatorId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                if ((await _menuItemService.ItemExistsByCreatorId(itemId, creatorId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                await _menuItemService.DeleteMenuItem(itemId, creatorId);

                return Ok(new
                {
                    status = 204
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
