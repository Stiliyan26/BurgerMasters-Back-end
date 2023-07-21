using Microsoft.AspNetCore.Identity;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }

        public ICollection<ApplicationUserMenuItem> CartItems { get; set; }
            = new List<ApplicationUserMenuItem>();
    }
}
