using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthdate { get; set; }
    }
}
