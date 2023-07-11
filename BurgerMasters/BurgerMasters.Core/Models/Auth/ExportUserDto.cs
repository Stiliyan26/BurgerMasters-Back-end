using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.Auth
{
    public class ExportUserDto
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Birthdate { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
