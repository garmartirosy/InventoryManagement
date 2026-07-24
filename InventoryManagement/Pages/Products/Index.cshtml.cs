using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InventoryManagement.Pages.Products
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        private readonly SupplierRepository _supplierRepository;

        public IndexModel(ProductRepository productRepository, ProductCategoryRepository productCategoryRepository, SupplierRepository supplierRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _supplierRepository = supplierRepository;
        }

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public Product Product { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            await LoadData();

            if (id.HasValue)
            {
                var product = await _productRepository.GetProductById(id.Value, GetUserId());

                if (product != null)
                    Product = product;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            if (Product.Id == 0)
                await _productRepository.CreateProduct(Product, GetUserId());
            else
                await _productRepository.UpdateProduct(Product, GetUserId());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _productRepository.DeleteProduct(id, GetUserId());

            return RedirectToPage();
        }

        private async Task LoadData()
        {
            var userId = GetUserId();

            Products = await _productRepository.GetProducts(userId);
            ViewData["ProductCategories"] = await _productCategoryRepository.GetProductCategories(userId);
            ViewData["Suppliers"] = await _supplierRepository.GetSuppliers(userId);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
