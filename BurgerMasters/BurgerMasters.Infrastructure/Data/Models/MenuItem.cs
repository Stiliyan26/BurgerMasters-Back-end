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
        [MaxLength(80)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public int ItemTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(ItemTypeId))]
        public ItemType ItemType { get; set; } = null!;

        [Required]
        public int PortionSize { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        [MaxLength(50)]
        public string CreatorId { get; set; } = null!;

        public ICollection<ApplicationUserMenuItem> ApplicationUserMenuItems { get; set; }
            = new List<ApplicationUserMenuItem>();
    }
}
