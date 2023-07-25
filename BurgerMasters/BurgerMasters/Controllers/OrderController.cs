using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace BurgerMasters.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("SentOrder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> SentOrder([FromBody] OrderViewModel orderInfo)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.ORDER_DATA_INAVLID_MSG,
                    status = 422
                });
            }

            try
            {
                await _orderService.CreateOrderAsync(orderInfo);

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

        [HttpGet("AllPendingOrders"), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AllPendingOrders([FromQuery] string adminId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (adminId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }
                IEnumerable<ExportOrderViewModel> orders = await _orderService
                    .GetAllPendingOrdersAsync();

                return Ok(new
                {
                    orders,
                    status = 200
                });
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("OrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> OrderById([FromQuery] string userId, Guid orderId)
        {
            try
            {
                string curretnIdentityId = GetUserId();

                if (userId != curretnIdentityId)
                {
                    return Conflict(new
                    {
                        errorMessage = ValidationConstants.ADMIN_ID_DIFFRENCE,
                        status = 409
                    });
                }

                var orderInfo = await _orderService.GetOrderByIdAsync(userId, orderId);

                if (orderInfo == null)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.ITEM_NOT_FOUND
                    });
                }

                return Ok(new
                {
                    orderInfo,
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
