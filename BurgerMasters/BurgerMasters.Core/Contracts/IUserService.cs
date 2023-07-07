using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BurgerMasters.Core.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(string username, string email,
            string password, DateTime birthdate);

        Task<SignInResult> LoginAsync(string email, string password);

        Task LogoutAsync();

        void SetUserIdentity(ExportUserDto userInfo, string userId);
    }
}
