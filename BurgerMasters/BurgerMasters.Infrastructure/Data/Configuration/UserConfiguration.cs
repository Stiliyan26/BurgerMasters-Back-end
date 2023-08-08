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

            var admin1 = new ApplicationUser()
            {
                Id = "a0407939-a95d-40a2-8db6-020d349bd2bb",
                UserName = "Stiliyan26",
                NormalizedUserName = "STILIYAN",
                Email = "stiliyan@gmail.com",
                NormalizedEmail = "STILIYAN@GMAIL.COM",
                Address = "Street: Vitosha Boulevard, Number: 10, Block: A",
                Birthdate = new DateTime(1998, 3, 15),
            };

            admin1.PasswordHash = hasher.HashPassword(admin1, "Admin#123");

            users.Add(admin1);

            var admin2 = new ApplicationUser()
            {
                Id = "c30d2c49-d677-42b3-9295-a0b1dae91806",
                UserName = "Peter12",
                NormalizedUserName = "PETER12",
                Email = "peter@gmail.com",
                NormalizedEmail = "PETER@GMAIL.COM",
                Address = "Street: Shipchenski Prohod Street, Number: 20, Block: B",
                Birthdate = new DateTime(1998, 3, 15),
            };

            admin2.PasswordHash = hasher.HashPassword(admin2, "Admin#123");

            users.Add(admin2);

            var user1 = new ApplicationUser()
            {
                Id = "e130798b-a521-45ad-85df-b232eaaadc09",
                UserName = "Bogdan16",
                NormalizedUserName = "BOGDAN16",
                Email = "bogdan@gmail.com",
                NormalizedEmail = "BOGDAN@GMAIL.COM",
                Address = "Street: Alexander Malinov Boulevard, Number: 30, Block: C",
                Birthdate = new DateTime(2003, 6, 29),
            };

            user1.PasswordHash = hasher.HashPassword(user1, "User#123");

            users.Add(user1);

            var user2 = new ApplicationUser()
            {
                Id = "d27076cc-efe7-4b1e-9730-e9630be4d3a6",
                UserName = "Pavlin14",
                NormalizedUserName = "PAVLIN14",
                Email = "pavlin@gmail.com",
                NormalizedEmail = "PAVLIN@GMAIL.COM",
                Address = " Street: Tsarigradsko Shose Boulevard, Number: 40, Block: D",
                Birthdate = new DateTime(2002, 12, 23),
            };

            user2.PasswordHash = hasher.HashPassword(user1, "User#123");

            users.Add(user2);

            return users;
        }
    }
}
