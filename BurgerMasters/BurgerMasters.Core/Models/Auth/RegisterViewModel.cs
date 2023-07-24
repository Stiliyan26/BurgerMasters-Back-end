using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Auth
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(16, MinimumLength = 5)]
        [RegularExpression(@"^[A-Za-z0-9]{5,16}$")]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [RegularExpression(@"^.{4,}@(abv|gmail|outlook|yahoo|hotmail)\.(bg|com|net|org)$")]
        public string Email { get; set; } = null!;

        [Required]
        public string Birthdate { get; set; } = null!;

        [Required]
        [MaxLength(80)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(20, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

    }
}
