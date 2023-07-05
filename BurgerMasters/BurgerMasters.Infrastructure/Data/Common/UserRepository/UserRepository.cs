using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

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
                //await _signInManager.SignInAsync(existingUser, false);

                /*var claims = new[]
                {   
                    new Claim(ClaimTypes.Name, existingUser.UserName),
                    new Claim(ClaimTypes.Email, existingUser.Email)
                    // Add any additional claims as needed
                };

                var identity = new ClaimsIdentity(claims, "ApplicationCookie");
                var principal = new ClaimsPrincipal(identity);

                _httpContextAccessor.HttpContext.User = principal;
*/
                return SignInResult.Success;
            }

            return SignInResult.Failed;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
