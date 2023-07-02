using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using BurgerMasters.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace BurgerMasters.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = UserRestrictionConstants.REQUIRE_UNIQUE_EMAIL;
                options.SignIn.RequireConfirmedEmail = UserRestrictionConstants.REQUIRE_CONFIRM_EMAIL;
                options.Password.RequiredLength = UserRestrictionConstants.PASSWORD_MIN_LENGTH;
            })
                .AddEntityFrameworkStores<BurgerMastersDbContext>()
                .AddDefaultTokenProviders();

            /*builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);*/
            /*builder.AddEntityFrameworkStores<BurgerMastersDbContext>();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();*/
        }
    }
}
