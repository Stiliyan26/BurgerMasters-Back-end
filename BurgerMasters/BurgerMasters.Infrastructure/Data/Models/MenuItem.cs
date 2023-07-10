using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerMasters.Infrastructure.Data.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int ItemTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(ItemTypeId))]
        public ItemType ItemType { get; set; } = null!;

        [Required]
        public int PortionSize { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<ApplicationUserMenuItem> ApplicationUserMenuItems { get; set; }
            = new List<ApplicationUserMenuItem>();
    }
}
