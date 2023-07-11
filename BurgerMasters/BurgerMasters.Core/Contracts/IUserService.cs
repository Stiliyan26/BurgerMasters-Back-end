using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BurgerMasters.Core.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace BurgerMasters.Core.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> RegisterAsync(RegisterViewModel model, DateTime birthdate);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<ExportUserDto> GetUserInfo(string email);

        void SetUserIdentity(ExportUserDto userInfo);
    }
}
