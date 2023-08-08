using BurgerMasters.Infrastructure.Data.Configuration;
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
    public class BurgerMastersDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public BurgerMastersDbContext(DbContextOptions<BurgerMastersDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        
        public DbSet<MenuItem> MenuItems { get; set; }

        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<ApplicationUserMenuItem> ApplicationUserMenuItems { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<MenuItem> ReviewMessages { get; set; } 

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

            modelBuilder.Entity<Order>()
                .Property(m => m.TotalPrice)
                .HasColumnType("decimal(18,2)");

            //Roles configuration
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            //User configuration
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            //Users Roles configuration
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            //Item configuration 
            modelBuilder.ApplyConfiguration(new ItemTypeConfiguration());
            //Menu items configuration
            modelBuilder.ApplyConfiguration(new MenuItemConfiguration());
            //Review configuration
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
