using Microsoft.AspNetCore.Identity;

namespace BurgerMasters.Infrastructure.Data.Common.UserRepository
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterAsync(string username, string email, string address,
            string password, DateTime birthdate);

        Task<SignInResult> LoginAsync(string email, string password);
    }
}
