using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMasters.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        private IActionResult HandleInvalidModelState()
        {
            return UnprocessableEntity(new
            {
                errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                status = 422
            });
        }

        private IActionResult NotFoundHandler()
        {
            return NotFound(new
            {
                errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                status = 404
            });
        }

        //Cheks if the user sending the request is the same as the logged in user
        private IActionResult ValidateUserId(string userId)
        {
            string currentIdentityId = GetUserId();

            if (userId != currentIdentityId)
            {
                return Conflict(new
                {
                    errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                    status = 409
                });
            }

            return null;
        }

        //Check if the item is created by the Current Admin
        private async Task<IActionResult> CheckItemExistsByCreatorId(int itemId, string creatorId)
        {
            if ((await _adminService.ItemExistsByCreatorIdAsync(itemId, creatorId)) == false)
            {
                return NotFoundHandler();
            }

            return null;
        }

        //Validatiom template
        private async Task<IActionResult> ProcessActionResult
            (Func<Task<IActionResult>> action, int itemId, string userId)
        {
            if (userId != null)
            {
                IActionResult userIdValidationResult = ValidateUserId(userId);

                if (userIdValidationResult != null)
                {
                    return userIdValidationResult;
                }
            }

            try
            {
                if (itemId != -1)
                {
                    IActionResult itemExistsValidationResult = 
                        await CheckItemExistsByCreatorId(itemId, userId);

                    if (itemExistsValidationResult != null)
                    {
                        return itemExistsValidationResult;
                    }
                }

                return action().Result;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
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
                return HandleInvalidModelState();
            }

            return await ProcessActionResult(async () =>
            {
                await _adminService.CreateMenuItemAsync(model, userId);

                return Ok(new
                {
                    itemType = model.ItemType,
                    status = 200
                });
            }, -1, userId);
        }

        [HttpGet("MyItemsByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MyItemsByType([FromQuery] string creatorId, string itemType)
        {
            return await ProcessActionResult(async () =>
            {
                IEnumerable<MenuItemViewModel> myItems = await _adminService
                    .GetCreatorItemsByTypeAsync(creatorId, itemType);

                return Ok(new
                {
                    myItems,
                    status = 200
                });
            }, -1, creatorId);
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
            return await ProcessActionResult(async () =>
            {
                IEnumerable<MenuItemViewModel> items =
                    await _adminService
                        .GetFourSimilarItemsByTypeAndCreatorAsync(itemType, itemId, creatorId);

                return Ok(new
                {
                    items,
                    status = 200,
                });
            }, itemId, creatorId);
        }

        [HttpGet("CreatorItemById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]

        public async Task<IActionResult> CreatorItemById([FromQuery] int itemId, string creatorId)
        {
            return await ProcessActionResult(async () =>
            {
                DetailsMenuItemViewModel item = await _adminService
                    .CreatorItemByIdAsync(itemId, creatorId);

                return Ok(new
                {
                    item,
                    status = 200
                });
            }, itemId, creatorId);
        }

        [HttpGet("EditItemInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditItemInfo([FromQuery] int itemId, string creatorId)
        {
            return await ProcessActionResult(async () =>
            {
                ViewEditItemInfoViewModel item = await _adminService
                    .GetEditItemInfoByItemIdAsync(itemId, creatorId);

                return Ok(new
                {
                    item,
                    status = 200
                });
            }, itemId, creatorId);
        }

        [HttpPut("EditMenuItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> EditMenuItem([FromBody] FormMenuItemViewModel model,
            [FromQuery] int itemId, string creatorId)
        {
            if (!ModelState.IsValid)
            {
                return HandleInvalidModelState();
            }

            return await ProcessActionResult(async () =>
            {
                await _adminService.EditMenuItemAsync(model, itemId, creatorId);

                return Ok(new
                {
                    itemId,
                    status = 200
                });
            }, itemId, creatorId);
        }


        [HttpPatch("DeleteItem")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteItem([FromQuery] int itemId, string creatorId)
        {
            return await ProcessActionResult(async () =>
            {
                await _adminService.DeleteMenuItemAsync(itemId, creatorId);

                return Ok(new
                {
                    status = 204
                });
            }, itemId, creatorId);
        }
    }
}
