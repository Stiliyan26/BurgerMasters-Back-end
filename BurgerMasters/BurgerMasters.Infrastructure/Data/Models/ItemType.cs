
using System.ComponentModel.DataAnnotations;


namespace BurgerMasters.Infrastructure.Data.Models
{
    public class ItemType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public IEnumerable<MenuItem> MenuItems { get; set; } = 
            new List<MenuItem>();
    }
}
