using BurgerMasters.Constants;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Transactions;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace BurgerMasters.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

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
                return HandleInvalidModelState();
            }

            return await ProcessActionResult(async () =>
            {
                await _orderService.CreateOrderAsync(orderInfo);

                return Ok(new
                {
                    status = 200
                });
            }, orderInfo.UserId);
        }

        [HttpGet("AllOrdersByStatus"), Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(IEnumerable<ExportOrderViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AllOrdersByStatus([FromQuery] string adminId, bool isPending)
        {
            return await ProcessActionResult(async () =>
            {
                var orders = await _orderService.GetAllOrdersByStatus(isPending);

                if (orders == null)
                {
                    return NotFoundHandler();
                }

                return Ok(new
                {
                    orders,
                    status = 200
                });
            }, adminId);
        }

        [HttpGet("OrderById")]
        [ProducesResponseType(typeof(OrderDetailsViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> OrderById([FromQuery] string userId, Guid orderId)
        {
            return await ProcessActionResult(async () =>
            {
                var orderInfo = await _orderService.GetOrderDetailsByIdAsync(orderId);

                if (orderInfo == null)
                {
                    return NotFoundHandler();
                }

                return Ok(new
                {
                    orderInfo,
                    status = 200
                });
            }, userId);
        }


        [HttpPatch("AcceptOrder"), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AcceptOrder(
            [FromQuery] string adminId,
            [FromBody] Guid orderId)
        {
            return await ProcessActionResult(async () =>
            {
                await _orderService.AcceptOrderAsync(orderId);

                return Ok(new
                {
                    status = 200
                });
            }, adminId);
        }

        [HttpPatch("UnacceptOrder"), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UnacceptOrder(
            [FromQuery] string adminId,
            [FromBody] Guid orderId)
        {
            return await ProcessActionResult(async () =>
            {
                await _orderService.UnacceptOrderAsync(orderId);

                return Ok(new
                {
                    status = 200
                });
            }, adminId);
        }

        [HttpPatch("DeclineOrder"), Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeclineOrder(
            [FromQuery] string adminId,
            [FromBody] Guid orderId)
        {
            return await ProcessActionResult(async () =>
            {
                await _orderService.DeclineOrderAsync(orderId);

                return Ok(new
                {
                    status = 200
                });
            }, adminId);
        }

        [HttpGet("AllOfMyOrders")]
        [ProducesResponseType(typeof(IEnumerable<ExportOrderViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AllOfMyOrders([FromQuery] string userId)
        {
            return await ProcessActionResult(async () =>
            {
                var orders = await _orderService.GetAllOrdersByUserId(userId);

                if (orders == null)
                {
                    return NotFoundHandler();
                }

                return Ok(new
                {
                    orders,
                    status = 200
                });
            }, userId);
        }

        //Helper methods

        private IActionResult HandleInvalidModelState()
        {
            return UnprocessableEntity(new
            {
                errorMessage = ValidationConstants.ORDER_DATA_INAVLID_MSG,
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

        //Validation template
        private async Task<IActionResult> ProcessActionResult
           (Func<Task<IActionResult>> action, string? userId = null)
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
                return action().Result;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
