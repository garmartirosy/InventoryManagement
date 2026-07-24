using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }

}
