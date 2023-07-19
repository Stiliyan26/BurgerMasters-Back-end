using BurgerMasters.Constants;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Auth;

namespace BurgerMasters.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(
            IUserService userService,
            ITokenService tokenService,
            UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Response type with status and (error message/s or userInfo)</returns>
        [HttpPost("Register"), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                    status = 422
                });
            }

            string token;

            try
            {
                DateTime validBirthdate;

                bool isValidBirthdate = DateTime
                        .TryParseExact(model.Birthdate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out validBirthdate);

                if (isValidBirthdate)
                {
                    var result = await _userService.RegisterAsync(model, validBirthdate);
                    //Checks if the user is created
                    if (result.Succeeded == false)
                    {
                        return Conflict(new
                        {
                            errors = result.Errors,
                            status = 409
                        });
                    }
                }

                ExportUserDto userInfo = await _userService.GetUserInfo(model.Email);
                //Generating Token
                token = _tokenService.GenerateToken(userInfo);
                //Setting User Identity
                _userService.SetUserIdentity(userInfo);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            return Ok(new
            {
                token,  
                status = 200
            });
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Response type with status and (errorMessage or userInfo)</returns>
        [HttpPost("Login"), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(new
                {
                    errorMessage = ValidationConstants.UNPROCESSABLE_ENTITY_ERROR_MSG,
                    status = 422
                });
            }

            string token;

            try
            {
                var userLoggedIn = await _userService.LoginAsync(model);

                if (userLoggedIn == null)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_USER_ERROR_MSG,
                        status = 404
                    });
                }

                if (userLoggedIn.Succeeded == false)
                {
                    return Unauthorized(new
                    {
                        errorMessage = ValidationConstants.UNAUTHORIZED_ERROR_MSG,
                        status = 401
                    });
                }

                ExportUserDto userInfo = await _userService.GetUserInfo(model.Email);
                //Generating Token
                token = _tokenService.GenerateToken(userInfo);
                //Setting User Identity
                _userService.SetUserIdentity(userInfo);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            return Ok(new
            {
                token,
                status = 200
            });
        }

        /// <summary>
        /// Logs out the currently authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

            return Ok();
        }

        /// <summary>
        /// Refreshes the token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("RefreshToken"), AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken([FromQuery] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            if (user != null)
            {
                {
                    ExportUserDto userInfo = new ExportUserDto
                    {
                        Id = userId,
                        Email = user.Email,
                        Username = user.UserName,
                        Birthdate = user.Birthdate.ToString("yyyy-MM-dd"),
                        Role = role ?? "",
                    };

                    string refreshToken = _tokenService.GenerateRefreshToken(userInfo);

                    return Ok(new
                    {
                        refreshToken,
                        status = 200
                    });
                }
            }

            return BadRequest("Refresh token generator failed!");
        }
    }
}
