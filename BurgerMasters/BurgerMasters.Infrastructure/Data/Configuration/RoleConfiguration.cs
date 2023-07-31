using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(CreateRoles());
        }

        public static List<IdentityRole> CreateRoles()
        {
            return new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = "453a4524-0cd1-46e6-abde-3219df401504",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "a439eb91-8c15-4e7a-abef-7f4ebc004826",
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
        }
    }
}
