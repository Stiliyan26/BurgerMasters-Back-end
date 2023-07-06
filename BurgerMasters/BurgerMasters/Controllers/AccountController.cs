using BurgerMasters.Constants;
using BurgerMasters.Core.Models;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using BurgerMasters.Core.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BurgerMasters.Controllers
{
    [EnableCors]
    [Route("/api/[controller]")]
    [ApiController]
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

            ExportUserDto userInfo;

            try
            {
                DateTime validBirthdate;

                bool isValidBirthDate = DateTime
                        .TryParseExact(model.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out validBirthdate);

                if (isValidBirthDate)
                {
                    var result = await _userService.RegisterAsync(
                        model.UserName,
                        model.Email,
                        model.Password,
                        validBirthdate);

                    if (result.Succeeded == false)
                    {
                        return Conflict(new
                        {
                            errors = result.Errors,
                            status = 409
                        });
                    }
                }

                userInfo = new ExportUserDto()
                {
                    Username = model.UserName,
                    Email = model.Email,
                    Birthday = model.Birthday
                };
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            return Ok(new
            {
                userInfo,
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

            ExportUserDto userInfo;
            string token;

            try
            {
                var userLoggedIn = await _userService.LoginAsync(model.Email, model.Password);

                if (userLoggedIn == null)
                {
                    return NotFound(new
                    {
                        errorMessage = ValidationConstants.NOT_FOUND_ERROR_MSG,
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

                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(existingUser);
                var role = roles.FirstOrDefault();

                userInfo = new ExportUserDto()
                {
                    Username = existingUser.UserName,
                    Email = existingUser.Email,
                    Birthday = existingUser.Birthday.ToString("yyyy-MM-dd") ?? string.Empty,
                    Role = role ?? ""
                };

                token = _tokenService.GenerateToken(userInfo);
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

        [HttpGet("Admins")]
        [Authorize(Roles = "Admin")]
        public IActionResult AdminEndpoint()
        {
            return Ok("You accesed Admin endpoint!");
        }

        /// <summary>
        /// Logs out the currently authenticated user
        /// </summary>
        /// <returns></returns>
        [HttpGet("Logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();

            return Ok();
        }
    }
}
