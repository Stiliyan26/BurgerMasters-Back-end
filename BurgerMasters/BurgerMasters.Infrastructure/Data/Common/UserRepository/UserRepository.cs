using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BurgerMasters.Infrastructure.Data.Common.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserRepository(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> RegisterAsync(string username, string email,
            string password, DateTime birthdate)
        {
            var user = new ApplicationUser()
            {
                UserName = username,
                Email = email,  
                Birthday = birthdate
            };

            var result = await _userManager.CreateAsync(user, password);

            return result;
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser == null)
            {
                return null;
            }

            var result = await _signInManager.PasswordSignInAsync(existingUser, password,
                false, false);

            if (result.Succeeded)
            {
                return SignInResult.Success;
            }

            return SignInResult.Failed;
        }
    }
}
