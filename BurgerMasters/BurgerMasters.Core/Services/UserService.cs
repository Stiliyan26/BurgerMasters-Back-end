using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models.Auth;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BurgerMasters.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> _userManager;

        public UserService(
            IUserRepository userRepository, 
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _userRepo = userRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model, DateTime birthdate)
        {
            return await _userRepo
                .RegisterAsync(model.UserName, model.Email, model.Address, model.Password, birthdate);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _userRepo.LoginAsync(model.Email, model.Password);      
        }

        /// <summary>
        /// Reset User Identity
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            _httpContextAccessor.HttpContext!.User = new ClaimsPrincipal(new ClaimsIdentity());
        }

        /// <summary>
        /// Gets information about the user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>ExportUserDto</returns>
        public async Task<ExportUserDto> GetUserInfo(string email)
        {
            var existingUser = await _userManager.FindByEmailAsync(email);
            var userId = existingUser.Id;

            var roles = await _userManager.GetRolesAsync(existingUser);
            var role = roles.FirstOrDefault();

            return new ExportUserDto()
            {
                Id = userId,
                Username = existingUser.UserName,
                Address = existingUser.Address,
                Email = existingUser.Email,
                Birthdate = existingUser.Birthdate.ToString("yyyy-MM-dd") ?? string.Empty,
                Role = role ?? ""
            };
        }
        /// <summary>
        /// Set User Identity
        /// </summary>
        /// <param name="userInfo"></param>
        public void SetUserIdentity(ExportUserDto userInfo)
        {
            var claims = _tokenService.GetClaims(userInfo);
            var userIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var claimPrinciple = new ClaimsPrincipal(userIdentity);

            _httpContextAccessor.HttpContext!.User = claimPrinciple;
        }
    }
}
