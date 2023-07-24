using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }

        [MaxLength(80)]
        public string Address { get; set; } = null!;

        public ICollection<ApplicationUserMenuItem> CartItems { get; set; }
            = new List<ApplicationUserMenuItem>();
    }
}
