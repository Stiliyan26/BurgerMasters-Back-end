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
            menuItems.AddRange(CreateDrinks());
            menuItems.AddRange(CreateHotdogs());
            menuItems.AddRange(CreateGrills());
            menuItems.AddRange(CreateSalads());
            menuItems.AddRange(CreateSandwiches());

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
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Surf'n'Turf",
                    ImageUrl = "SurfNTurff.webp",
                    PortionSize = 370,
                    Description = @"Homemade Brioche Bread, Black Angus ground beef, Aioli sauce, Shrimp, Iceberg",
                    Price = 19.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Burger Pie",
                    ImageUrl = "BurgerPie.webp",
                    PortionSize = 380,
                    Description = @"Black Angus ground beef, American cheese, Cheddar, Jalapeno",
                    Price = 19.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Burger Pie",
                    ImageUrl = "BurgerPie.webp",
                    PortionSize = 380,
                    Description = @"Black Angus ground beef, American cheese, Cheddar, Jalapeno",
                    Price = 19.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Smokey Whiskey Cheeseburger",
                    ImageUrl = "SmokeyWhiskeyCheeseburger.webp",
                    PortionSize = 480,
                    Description = @"Homemade Brioche Bread, Burger sauce, 200g Black Angus ground beef patties with brown sugar and whiskey, Smokey BBQ, Mushrooms with olive oil, onion and garlic, Crispy bacon, Very cheddar, Fresh tomato",
                    Price = 23.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Triple Cheeseburger",
                    ImageUrl = "TrippleCheese.webp",
                    PortionSize = 550,
                    Description = @"Homemade Brioche Bread, BBQ sauce with bourbon, Our pickle, Black Angus ground beef with blue cheese, mozzarella and lots of cheddar, wrapped in bacon, Grilled onions",
                    Price = 23.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Bacon Jam Burger",
                    ImageUrl = "BaconJam.webp",
                    PortionSize = 450,
                    Description = @"Homemade Brioche Bread, Homemade bourbon BBQ sauce, Crispy iceberg, Black Angus ground beef patties, Very cheddar, Bacon Apricot Marmalade",
                    Price = 19.49m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                 new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "BBQ Pulled Pork",
                    ImageUrl = "BBQPulledPork.webp",
                    PortionSize = 350,
                    Description = @"Homemade Brioche Bread, Colsloe, Slow Roasted Pork Shoulder, Caramelized onions, Melted cheddar, Homemade bourbon BBQ sauce",
                    Price = 14.40m,
                    ItemTypeId = 1,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                }
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
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Mozzarella sticks",
                    ImageUrl = "MozzarellaSticks.webp",
                    PortionSize = 170,
                    Description = @"Mozzarella cheese, Blueberry jam, All purpose flour",
                    Price = 9.99m,
                    ItemTypeId = 3,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
            };
        }

        private static List<MenuItem> CreateDrinks()
        {
            return new List<MenuItem>()
            {
                new MenuItem() 
                {
                    Id = uniqueId++,
                    Name = "Coca-Cola no sugar",
                    ImageUrl = "CocaColaNoSugar.webp",
                    PortionSize = 330,
                    Description = @"Carbonated Water, Phosphoric Acid, Sweeteners (Aspartame, Acesulfame Potassium), Natural Flavors (including Caffeine)",
                    Price = 2.50m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Coca-Cola Original",
                    ImageUrl = "Classic.webp",
                    PortionSize = 330,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Natural Flavors",
                    Price = 2.50m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Coca-cola cherry",
                    ImageUrl = "CherryVanilla.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Potassium Benzoate",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Coca-cola orange vanilla",
                    ImageUrl = "OrangeVanilla.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Caffeine, Sodium Benzoat",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Coca-cola vanila",
                    ImageUrl = "Vanilla.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Caramel Color, Phosphoric Acid, Natural Flavors (including Vanilla), Caffeine",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Fanta strawberry",
                    ImageUrl = "FantaStrawberry.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Natural and Artificial Flavors, Citric Acid, Sodium Citrate, Red 40 (Color)",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Fanta berry",
                    ImageUrl = "FantaBerry.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Citric Acid, Natural and Artificial Flavors, Red 40 (Color), Blue 1 (Color)",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                }, 
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Fanta Grape",
                    ImageUrl = "FantaGrape.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Natural and Artificial Flavors, Citric Acid, Sodium Benzoate, Red 40 (Color), Blue 1 (Color)",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Fanta Peach",
                    ImageUrl = "FantaPeach.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Natural Flavors, Natural Flavors, Sodium Benzoate, Yellow 6 (Color), Red 40 (Color)",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Fanta Pineapple",
                    ImageUrl = "FantaPineapple.webp",
                    PortionSize = 355,
                    Description = @"Carbonated Water, High Fructose Corn Syrup, Natural Flavors, Sodium Benzoat, Potassium Sorbate, Yellow 5 (Color), Yellow 5 (Color)",
                    Price = 4.90m,
                    ItemTypeId = 2,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
            };
        }

        private static List<MenuItem> CreateHotdogs()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Old Style Oklahoma",
                    ImageUrl = "Oklahoma.webp",
                    PortionSize = 200,
                    Description = @"Homemade bread, BBQ sauce, Grilled sausage with cheddar and bacon, Mustard, Onion, Oklahoma Mince, Homemade cheddar sauce",
                    Price = 12.49m,
                    ItemTypeId = 4,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Old Style Mac'n'cheese",
                    ImageUrl = "MacNcheese.webp",
                    PortionSize = 200,
                    Description = @"Homemade Brioche Bread, Grilled beef sausage, Smoked bacon, Mustard, Pasta with American cheese",
                    Price = 11.49m,
                    ItemTypeId = 4,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Old Style Mineapolis",
                    ImageUrl = "Mineapolis.webp",
                    PortionSize = 200,
                    Description = @"Homemade brioche bread, Smoked pork sausage, Smoked bacon, Coleslaw salad, BBQ sauce, Mustard",
                    Price = 11.49m,
                    ItemTypeId = 4,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "CornDog",
                    ImageUrl = "CornDog.webp",
                    PortionSize = 250,
                    Description = @"Beef sausage breaded with cornmeal, egg, mustard and honey, Coleslaw salad, Mustard",
                    Price = 11.49m,
                    ItemTypeId = 4,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
            };
        }

        private static List<MenuItem> CreateGrills()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Black Angus Grill",
                    ImageUrl = "BlackAngus.webp",
                    PortionSize = 460,
                    Description = @"160 g of Black Angus ground beef, Spicy beans with bacon, molasses and spices, Coleslaw",
                    Price = 18.90m,
                    ItemTypeId = 5,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Smoked pork sausage",
                    ImageUrl = "PorkSausage.webp",
                    PortionSize = 400,
                    Description = @"2 smoked pork sausages, Spicy beans with bacon, molasses and spices, Coleslaw",
                    Price = 19.10m,
                    ItemTypeId = 5,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Double Black Angus Gril",
                    ImageUrl = "BlackAngus2.webp",
                    PortionSize = 640,
                    Description = @"2 patties of 160g each. from Black Angus ground beef, Spicy beans with bacon, molasses and spices, Coleslaw",
                    Price = 23.90m,
                    ItemTypeId = 5,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
            };
        }

        private static List<MenuItem> CreateSalads()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Salad Coleslaw",
                    ImageUrl = "Coleslaw.webp",
                    PortionSize = 250,
                    Description = @"Red cabbage,White Cabbage,Carrot,Mayonnaise dressing",
                    Price = 5.99m,
                    ItemTypeId = 6,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Salad Cobb",
                    ImageUrl = "Cobb.webp",
                    PortionSize = 400,
                    Description = @"Lettuce,Tomato,Blue cheese,Avocado,Chicken fillet,Bacon,Quail eggs,Red onion,Dressing",
                    Price = 14.90m,
                    ItemTypeId = 6,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
            };
        }

        private static List<MenuItem> CreateSandwiches()
        {
            return new List<MenuItem>()
            {
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Fried Pork Priviledge",
                    ImageUrl = "FiredPork.webp",
                    PortionSize = 390,
                    Description = @"Homemade American bread toasted with butter, Breaded sirloin (pork), Mustard, White onion, Colsloe, Tomato",
                    Price = 13.90m,
                    ItemTypeId = 7,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Sloppy Joe",
                    ImageUrl = "SloppyJoe.webp",
                    PortionSize = 320,
                    Description = @"Homemade Brioche Bread, American potato salad with mustard and bacon, Ground beef with Sloppy Joe sauce",
                    Price = 11.49m,
                    ItemTypeId = 7,
                    IsActive = true,
                    CreatorId = "a0407939-a95d-40a2-8db6-020d349bd2bb"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Rusty Bacon",
                    ImageUrl = "RustyBacon.webp",
                    PortionSize = 380,
                    Description = @"Homemade Brioche Bread, Coleslaw, Crispy bacon, BBQ sauce with bourbon, Caramelized onions",
                    Price = 9.99m,
                    ItemTypeId = 7,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "Scrambled eggs with bacon",
                    ImageUrl = "ScrambledEggs.webp",
                    PortionSize = 270,
                    Description = @"Toast slices with smoked mayonnaise, pan-fried, Lettuce, Tomato",
                    Price = 10.99m,
                    ItemTypeId = 7,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
                new MenuItem()
                {
                    Id = uniqueId++,
                    Name = "American Grill and Cheese",
                    ImageUrl = "GrillnCheese.webp",
                    PortionSize = 200,
                    Description = @"Toast slices with smoked mayonnaise, pan-fried, Cheddar, Mozzarella",
                    Price = 10.09m,
                    ItemTypeId = 7,
                    IsActive = true,
                    CreatorId = "c30d2c49-d677-42b3-9295-a0b1dae91806"
                },
            };
        }
    }
}
