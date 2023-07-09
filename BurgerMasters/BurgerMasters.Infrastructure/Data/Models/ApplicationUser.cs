using Microsoft.AspNetCore.Identity;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
    }
}
