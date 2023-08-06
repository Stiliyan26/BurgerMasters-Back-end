using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.MenuItemModels;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace BurgerMasters.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;
        private readonly IMenuItemService _menuItemService;
        public CartController(
            ICartService cartService,
            IMenuItemService menuItemService)
        {
            _cartService = cartService;
            _menuItemService = menuItemService;
        }

        [HttpPost("AddItemToCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> AddItemToCart([FromBody] CartInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return HandleInvalidModelState();
            }

            return await ProcessActionResult(async () =>
            {
                await _cartService.AddItemToUserCartAsync(model);

                return Ok(new
                {
                    status = 200
                });
            }, model.ItemId, model.UserId);
        }

        [HttpGet("AllCartItems")]
        [ProducesResponseType(typeof(IEnumerable<CartItemInfoViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AllCartItems([FromQuery] string userId)
        {
            return await ProcessActionResult(async () =>
            {
                var cartItems = await _cartService
                    .GetAllCartItemsByUserIdAsync(userId);

                if (cartItems == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    cartItems,
                    status = 200
                });
            }, -1, userId);
        }


        [HttpDelete("RemoveCartItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RemoveCartItem([FromQuery] int itemId, string userId)
        {
            return await ProcessActionResult(async () =>
            {
                await _cartService.RemoveItemFromCartByIdAsync(itemId, userId);

                return Ok(new
                {
                    status = 200
                });
            }, itemId, userId);
        }


        [HttpDelete("CleanUpCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CleanUpCart([FromQuery] string userId)
        {
            return await ProcessActionResult(async () =>
            {
                await _cartService.CleanUpCartAsync(userId);

                return Ok(new
                {
                    status = 200
                });
            }, -1, userId);
        }


        private IActionResult HandleInvalidModelState()
        {
            return UnprocessableEntity(new
            {
                errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                status = 422
            });
        }

        //Helper methods

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

        //Checks if the item exists
        private async Task<IActionResult> ItemExistsHandler(int itemId)
        {
            if ((await _menuItemService.ItemExistsAsync(itemId)) == false)
            {
                return NotFound(new
                {
                    errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                    status = 404
                });
            }

            return null;
        }

        //Validation template
        private async Task<IActionResult> ProcessActionResult
            (Func<Task<IActionResult>> action, int itemId = -1, string? userId = null)
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
                        await ItemExistsHandler(itemId);

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
    }
}
