using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InventoryManagement.Pages.Inventories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly InventoryRepository _inventoryRepository;
        private readonly ProductRepository _productRepository;

        public IndexModel(InventoryRepository inventoryRepository, ProductRepository productRepository)
        {
            _inventoryRepository = inventoryRepository;
            _productRepository = productRepository;
        }

        public IEnumerable<Inventory> Inventories { get; set; } = new List<Inventory>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public Inventory Inventory { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            await LoadData();

            if (id.HasValue)
            {
                var inventory = await _inventoryRepository.GetInventoryById(id.Value, GetUserId());

                if (inventory != null)
                    Inventory = inventory;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            if (Inventory.Id == 0)
                await _inventoryRepository.CreateInventory(Inventory, GetUserId());
            else
                await _inventoryRepository.UpdateInventory(Inventory, GetUserId());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _inventoryRepository.DeleteInventory(id, GetUserId());

            return RedirectToPage();
        }

        private async Task LoadData()
        {
            var userId = GetUserId();

            Inventories = await _inventoryRepository.GetInventories(userId);
            Products = await _productRepository.GetProducts(userId);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
