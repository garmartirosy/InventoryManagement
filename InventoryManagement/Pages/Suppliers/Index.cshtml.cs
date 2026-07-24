using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InventoryManagement.Pages.Suppliers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SupplierRepository _supplierRepository;

        public IndexModel(SupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<Supplier> Suppliers { get; set; } = new List<Supplier>();

        [BindProperty]
        public Supplier Supplier { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            await LoadData();

            if (id.HasValue)
            {
                var supplier = await _supplierRepository.GetSupplierById(id.Value, GetUserId());

                if (supplier != null)
                    Supplier = supplier;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            if (Supplier.Id == 0)
                await _supplierRepository.CreateSupplier(Supplier, GetUserId());
            else
                await _supplierRepository.UpdateSupplier(Supplier, GetUserId());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _supplierRepository.DeleteSupplier(id, GetUserId());

            return RedirectToPage();
        }

        private async Task LoadData()
        {
            Suppliers = await _supplierRepository.GetSuppliers(GetUserId());
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
