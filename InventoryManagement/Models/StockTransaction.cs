using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class StockTransaction
    {
        public int Id { get; set; }

        [Display(Name = "Product")]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        public string? Description { get; set; }
        public string ProductName { get; set; } = string.Empty;
    }

}
