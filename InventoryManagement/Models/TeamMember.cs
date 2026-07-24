using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models
{
    public class TeamMember
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(256)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;
    }
}