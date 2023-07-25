using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.MenuItem;
using BurgerMasters.Core.Models.Transactions;
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
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                    status = 422
                });
            }

            try
            {
                string curretnIdentityId = GetUserId();

                if (model.UserId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.USER_ID_DIFFRENCE,
                        status = 409
                    });
                }

                if ((await _menuItemService.ItemExistsAsync(model.ItemId)) == false)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ITEM_ERROR_MSG,
                        status = 404
                    });
                }

                await _cartService.AddItemToUserCartAsync(model);

                return Ok(new
                {
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("AllCartItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AllCartItems([FromQuery] string userId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (userId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.USER_ID_DIFFRENCE,
                        status = 409
                    });
                }

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
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpDelete("RemoveCartItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RemoveCartItem([FromQuery] int itemId, string userId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (userId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.USER_ID_DIFFRENCE,
                        status = 409
                    });
                }

                await _cartService.RemoveItemFromCartById(itemId, userId);

                return Ok(new
                {
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }


        [HttpDelete("CleanUpCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CleanUpCart([FromQuery] string userId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (userId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.USER_ID_DIFFRENCE,
                        status = 409
                    });
                }

                await _cartService.CleanUpCartAsync(userId);

                return Ok(new
                {
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
