using BurgerMasters.Infrastructure.Data.Models;
using BurgerMasters.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using BurgerMasters.Constants;
using Microsoft.Extensions.DependencyInjection;
using BurgerMasters.Core.Contracts;
using BurgerMasters.Core.Services;
using BurgerMasters.Infrastructure.Data.Common.UserRepository;
using BurgerMasters.Infrastructure.Data.Common.Repository;

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
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            // User services
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // General repository
            services.AddScoped<IRepository, Repository>();

            // Menu services
            services.AddScoped<IMenuItemService, MenuItemService>();

            // Cart service
            services.AddScoped<ICartService, CartService>();

            // Admin service
            services.AddScoped<IAdminService, AdminService>();

            // Order service
            services.AddScoped<IOrderService, OrderService>();
            //SiganlR Review messages
            services.AddScoped<IReviewService, ReviewService>();
        }
    }
}
