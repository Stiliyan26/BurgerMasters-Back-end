using BurgerMasters.Infrastructure.Data.Models;
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
    public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(CreateUserRole());
        }

        private static List<IdentityUserRole<string>> CreateUserRole()
        {
            return new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    UserId = "a0407939-a95d-40a2-8db6-020d349bd2bb",
                    RoleId = "453a4524-0cd1-46e6-abde-3219df401504"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "c30d2c49-d677-42b3-9295-a0b1dae91806",
                    RoleId = "453a4524-0cd1-46e6-abde-3219df401504"
                },
                new IdentityUserRole<string>()
                {
                    UserId = "e130798b-a521-45ad-85df-b232eaaadc09",
                    RoleId = "a439eb91-8c15-4e7a-abef-7f4ebc004826"
                }
            };
        }
    }
}
