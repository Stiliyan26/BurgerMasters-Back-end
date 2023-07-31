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
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(CreateApplicationUsersAsync()); 
        }

        private static List<ApplicationUser> CreateApplicationUsersAsync()
        {
            List<ApplicationUser> users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser()
            {
                Id = "a0407939-a95d-40a2-8db6-020d349bd2bb",
                UserName = "Admin12",
                NormalizedUserName = "ADMIN12",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Address = "Street: 17, bul. Cherni vrah",
                Birthdate = new DateTime(1998, 3, 15),
            };

            admin.PasswordHash = hasher.HashPassword(admin, "Admin#12");

            users.Add(admin);

            var user = new ApplicationUser()
            {
                Id = "e130798b-a521-45ad-85df-b232eaaadc09",
                UserName = "User13",
                NormalizedUserName = "USER13",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                Address = "Street: 17, bul. Cherni vrah",
                Birthdate = new DateTime(2003, 6, 29),
            };

            user.PasswordHash = hasher.HashPassword(user, "User#13");

            users.Add(user);

            return users;
        }
    }
}
