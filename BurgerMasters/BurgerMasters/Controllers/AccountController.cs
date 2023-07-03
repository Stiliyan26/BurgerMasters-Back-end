using BurgerMasters.Constants;
using BurgerMasters.Core.Models;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;

namespace BurgerMasters.Controllers
{
    [EnableCors]
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
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
                    ApplicationUser newApplicationUser = new ApplicationUser()
                    {
                        UserName = model.UserName,
                        Email = model.Email,
                        Birthday = validBirthdate,
                    };

                    var result = await _userManager.CreateAsync(newApplicationUser, model.Password);

                    if (result.Succeeded == false)
                    {
                        return Conflict(new {
                            errors = result.Errors, 
                            status = 409 
                        });
                    }

                    await _signInManager.SignInAsync(newApplicationUser, isPersistent: false);
                }

                userInfo = new ExportUserDto()
                {
                    Username = model.UserName,
                    Email = model.Email,
                    Birthday = model.Birthday
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new {
                userInfo,
                status = 200 
            });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            ApplicationUser existingUser = await _userManager.FindByEmailAsync(model.Email);

            if (existingUser == null)
            {
                return NotFound(ValidationConstants.NOT_FOUND_ERROR_MSG);
            }

            var result = await _signInManager.PasswordSignInAsync(existingUser, model.Password, false, false);

            if (result.Succeeded == false)
            {
                return Unauthorized(new {
                    errorMessage = ValidationConstants.UNAUTHORIZED_ERROR_MSG,
                    status = 401 
                });
            }

            ExportUserDto userInfo = new ExportUserDto()
            {
                Username = existingUser.UserName,
                Email = existingUser.Email,
                Birthday = existingUser.Birthday.ToString("yyyy-MM-dd") ?? string.Empty
            };

            return Ok(new
            {
                userInfo,
                status = 200
            });
        }

        [HttpGet]
        [Route("Logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
    }
}
