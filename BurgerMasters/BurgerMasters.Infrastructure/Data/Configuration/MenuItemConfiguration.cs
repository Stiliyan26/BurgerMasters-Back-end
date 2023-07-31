using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data.Configuration
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        private static int uniqueId = 1;
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasData(CreateMenuItems());
        }

        private static List<MenuItem> CreateMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            menuItems.AddRange(CreateBurgers());
            menuItems.AddRange(CreateFries());

            return menuItems;
        }

        private static List<MenuItem> CreateBurgers()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "THE JUICY LUCY",
                    ImageUrl = "JuicyLucy.webp",
                    PortionSize = 350,
                    Description = @"Homemade Brioche Bread, Burger sauce, Black Angus ground beef mixed with cheddar, Melted Irish red cheddar, Crispy bacon, Caramelized onions",
                    Price = 17.40m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Pineapple Bacon Run",
                    ImageUrl = "PineappleBaconRun.webp",
                    PortionSize = 380,
                    Description = @"Homemade Brioche Bread, Pineapple butter with rum, 200g Black Angus ground beef, Grilled pineapple, Crispy bacon with brown sugar",
                    Price = 19.40m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "RUSTY SAVAGE",
                    ImageUrl = "RustySavage.webp",
                    PortionSize = 630,
                    Description = @"Homemade Brioche Bread, Burger sauce, Colsloe,Ground beef Black Angus x2, Melted Irish red cheddar x2, Crispy bacon, Homemade pickle, Caramelized onions",
                    Price = 27.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "THUNDER",
                    ImageUrl = "Tunder.webp",
                    PortionSize = 380,
                    Description = @"Homemade Brioche Bread, Grilled onions, Thunder spicy sauce (tomato sauce, jalapeño, pickle, sweet apricot), Black Angus ground beef, Homemade cheddar sauce, Homemade pickle",
                    Price = 17.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "American Cheese Burger",
                    ImageUrl = "American.webp",
                    PortionSize = 320,
                    Description = @"Homemade Brioche Bread, Burger sauce, Iceberg, Black Angus ground beef, Crispy bacon, American cheese, Tomato, Pickled red onion",
                    Price = 14.99m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
            };

        }

        private static List<MenuItem> CreateFries()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Regular Fries",
                    ImageUrl = "RegularFries.webp",
                    PortionSize = 170,
                    Description = @"Homemade potatoes, Vegetable Oil, Salt",
                    Price = 4.99m,
                    ItemTypeId = 3,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Spicy Fries",
                    ImageUrl = "SpicyFries.webp",
                    PortionSize = 240,
                    Description = @"Homemade potatoes, Vegetable Oil, Salt, Homemade hot sauce",
                    Price = 6.99m,
                    ItemTypeId = 3,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Cheddar Fries",
                    ImageUrl = "CheddarFries.webp",
                    PortionSize = 300,
                    Description = @"Homemade potatoes, Vegetable Oil, Salt, Pepper, House fried sauce and jalapenos",
                    Price = 7.99m,
                    ItemTypeId = 3,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Breaded onion rings",
                    ImageUrl = "OnionRings.webp",
                    PortionSize = 250,
                    Description = @"Onions, Flour, Bread crumbs, Salt and pepper",
                    Price = 6.99m,
                    ItemTypeId = 3,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
            };
        }
    }
}
