using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Display(Name = "Product")]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        public string ProductName { get; set; } = string.Empty;
    }

}
