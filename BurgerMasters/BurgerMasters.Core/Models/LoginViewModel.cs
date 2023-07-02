using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^.{4,}@(abv|gmail|outlook|yahoo|hotmail)\.(bg|com|net|org)$")]
        public string Email { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
