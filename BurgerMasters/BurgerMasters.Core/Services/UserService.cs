
using BurgerMasters.Core.Contracts;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BurgerMasters.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> RegisterAsync(string username, string email,
            string password, DateTime birthdate)
        {
            return await _userRepository.RegisterAsync(username, email, password, birthdate);
        }

        public async Task<SignInResult> LoginAsync(string email, string password)
        {
            var result = await _userRepository.LoginAsync(email, password);

            return result;
        }

        public async Task LogoutAsync()
        {
            await _userRepository.LogoutAsync();
        }
    }
}
