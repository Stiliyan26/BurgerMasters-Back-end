using BurgerMasters.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data.Configuration
{
    public class ItemTypeConfiguration : IEntityTypeConfiguration<ItemType>
    {
        public void Configure(EntityTypeBuilder<ItemType> builder)
        {
            builder
                .HasData(CreateItemTypes());
        }

        private static List<ItemType> CreateItemTypes()
        {
            List<ItemType> items = new List<ItemType>()
            {
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
                },
                new ItemType()
                {
                    Id = 4,
                    Name = "Hotdog"
                },
                new ItemType()
                {
                    Id = 5,
                    Name = "Grill"
                },
                new ItemType()
                {
                    Id = 6,
                    Name = "Salad"
                },
                new ItemType()
                {
                    Id = 7,
                    Name = "Sandwich"
                }
            };

            return items;
        }
    }
}
