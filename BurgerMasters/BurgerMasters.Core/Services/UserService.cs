using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Models;
using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace BurgerMasters.Core.Services
{
    public class UserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        public UserService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task RegisterUser(RegisterViewModel userModel)
        {
            DateTime validBirthdate;

            bool isValidBirthDate = DateTime
                    .TryParseExact(userModel.Birthday, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out validBirthdate);

            if (isValidBirthDate)
            {
                ApplicationUser newApplicationUser = new ApplicationUser()
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    Birthday = validBirthdate,
                };

                await _userManager.CreateAsync(newApplicationUser, userModel.Password);
            }
        }
    }
}
