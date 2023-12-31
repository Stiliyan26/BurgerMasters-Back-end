﻿using BurgerMasters.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Core.Models.MenuItemModels
{
    public class FormMenuItemViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 5)]
        [RegularExpression(@"^[A-Za-z\s'-]{5,50}$")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(80, MinimumLength = 5)]
        public string ImageUrl { get; set; } = null!;

        public string? ItemType { get; set; }

        [Required]
        [Range(150, 800)]
        public int PortionSize { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), "2.00", "30.00")]
        public decimal Price { get; set; } 
    }
}
