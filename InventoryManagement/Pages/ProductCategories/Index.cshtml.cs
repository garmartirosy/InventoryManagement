using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InventoryManagement.Pages.ProductCategories
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ProductCategoryRepository _productCategoryRepository;

        public IndexModel(ProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public IEnumerable<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        [BindProperty]
        public ProductCategory ProductCategory { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            await LoadData();

            if (id.HasValue)
            {
                var productCategory = await _productCategoryRepository.GetProductCategoryById(id.Value, GetUserId());

                if (productCategory != null)
                    ProductCategory = productCategory;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            if (ProductCategory.Id == 0)
                await _productCategoryRepository.CreateProductCategory(ProductCategory, GetUserId());
            else
                await _productCategoryRepository.UpdateProductCategory(ProductCategory, GetUserId());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _productCategoryRepository.DeleteProductCategory(id, GetUserId());

            return RedirectToPage();
        }

        private async Task LoadData()
        {
            ProductCategories = await _productCategoryRepository.GetProductCategories(GetUserId());
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
