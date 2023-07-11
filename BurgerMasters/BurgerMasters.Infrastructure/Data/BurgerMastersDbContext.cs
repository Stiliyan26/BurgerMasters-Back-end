using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data
{
    public class BurgerMastersDbContext : IdentityDbContext<ApplicationUser>
    {
        public BurgerMastersDbContext()
        {
        }

        public BurgerMastersDbContext(DbContextOptions<BurgerMastersDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<ApplicationUserMenuItem> ApplicationUserMenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ApplicationUserMenuItem>(ui =>
                {
                    ui.HasKey(x => new { x.ApplicationUserId, x.MenuItemId });
                });

            modelBuilder.Entity<MenuItem>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder
                .Entity<ItemType>()
                .HasData(
                    new ItemType()
                    {
                        Id = 1,
                        Name = "Burger"
                    },
                    new ItemType()
                    {
                        Id = 2,
                        Name = "Drink"
                    },
                    new ItemType()
                    {
                        Id = 3,
                        Name = "Fries"
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
