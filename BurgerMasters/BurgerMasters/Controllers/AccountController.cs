﻿using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BurgerMasters.Controllers
{
    [EnableCors]
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IUserService _userService;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        public AccountController(IUserService userService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            ExportUserDto userInfo;

            try
            {
                await _userService.RegisterUser(model);

                userInfo = new ExportUserDto()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Birthday = model.Birthday
                };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(userInfo);
        }

        [HttpPost]
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
                return NotFound("No existing user");
            }

            var result = await _signInManager.PasswordSignInAsync(existingUser, model.Password, false, false);

            if (result.Succeeded == false)
            {
                return Unauthorized("Inccorect Email or Password!");
            }

            ExportUserDto userInfo = new ExportUserDto()
            {
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Birthday = existingUser.Birthday.ToString("yyyy-MM-dd") ?? string.Empty
            };

            return Ok(userInfo);
        }
    }
}
