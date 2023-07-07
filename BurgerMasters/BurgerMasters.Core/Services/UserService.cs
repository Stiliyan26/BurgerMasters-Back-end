using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
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
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(
            IUserRepository userRepository, 
            ITokenService tokenService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IdentityResult> RegisterAsync(string username, string email,
            string password, DateTime birthdate)
        {
            return await _userRepository.RegisterAsync(username, email, password, birthdate);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            return await _userRepository.LoginAsync(email, password);      
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
        /// Set User identity
        /// </summary>
        /// <param name="userInfo"></param>
        public void SetUserIdentity(ExportUserDto userInfo, string userId)
        {
            var claims = _tokenService.GetClaims(userInfo, userId);
            var userIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var claimPrinciple = new ClaimsPrincipal(userIdentity);

            _httpContextAccessor.HttpContext!.User = claimPrinciple;
        }
    }
}
