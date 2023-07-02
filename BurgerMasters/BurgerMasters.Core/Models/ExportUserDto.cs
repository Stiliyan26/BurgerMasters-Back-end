﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models
{
    public class ExportUserDto
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Birthday { get; set; }

    }
}
