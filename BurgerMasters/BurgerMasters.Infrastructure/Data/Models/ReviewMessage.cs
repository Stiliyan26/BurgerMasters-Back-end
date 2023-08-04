using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class ReviewMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [MaxLength(100)]
        public string Message { get; set; }

        [Required]
        public DateTime SentDate { get; set; }
    }
}
