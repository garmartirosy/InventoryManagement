using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public ProductCategory? Category { get; set; }

        public string CategoryName { get; set; } = string.Empty;

        [Display(Name = "Supplier")]
        public int? SupplierId { get; set; }

        public Supplier? Supplier { get; set; }

        public string SupplierName { get; set; } = string.Empty;
    }

}
