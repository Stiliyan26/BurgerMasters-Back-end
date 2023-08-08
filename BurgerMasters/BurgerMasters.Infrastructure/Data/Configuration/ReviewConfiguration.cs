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
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewMessage>
    {
        public void Configure(EntityTypeBuilder<ReviewMessage> builder)
        {
            builder.HasData(CreateReviewMessges());
        }

        private static List<ReviewMessage> CreateReviewMessges()
        {
            return new List<ReviewMessage>()
            {
                new ReviewMessage()
                {
                    Id = 1,
                    UserId = "e130798b-a521-45ad-85df-b232eaaadc09",
                    SentDate = new DateTime(2023, 8, 2, 15, 15, 22),
                    Message = "The quality of the food is very good and the price matches the quality!"
                },
                new ReviewMessage()
                {
                    Id = 2,
                    UserId = "d27076cc-efe7-4b1e-9730-e9630be4d3a6",    
                    SentDate = new DateTime(2023, 7, 3, 16, 11, 23),
                    Message = "Absolutely loved dining at this restaurant!"
                }
            };
        }
    }
}
