using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InventoryManagement.Pages.StockTransactions
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly StockTransactionRepository _stockTransactionRepository;
        private readonly ProductRepository _productRepository;

        public IndexModel(StockTransactionRepository stockTransactionRepository, ProductRepository productRepository)
        {
            _stockTransactionRepository = stockTransactionRepository;
            _productRepository = productRepository;
        }

        public IEnumerable<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        [BindProperty]
        public StockTransaction StockTransaction { get; set; } = new();

        public async Task OnGetAsync(int? id)
        {
            await LoadData();

            if (id.HasValue)
            {
                var stockTransaction = await _stockTransactionRepository.GetStockTransactionById(id.Value, GetUserId());

                if (stockTransaction != null)
                    StockTransaction = stockTransaction;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (StockTransaction.Amount == 0)
            {
                ModelState.AddModelError("StockTransaction.Amount", "Amount cannot be zero.");
            }

            if (!ModelState.IsValid)
            {
                await LoadData();
                return Page();
            }

            if (StockTransaction.Id == 0)
                await _stockTransactionRepository.CreateStockTransaction(StockTransaction, GetUserId());
            else
                await _stockTransactionRepository.UpdateStockTransaction(StockTransaction, GetUserId());

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _stockTransactionRepository.DeleteStockTransaction(id, GetUserId());

            return RedirectToPage();
        }

        private async Task LoadData()
        {
            var userId = GetUserId();

            StockTransactions = await _stockTransactionRepository.GetStockTransactions(userId);
            Products = await _productRepository.GetProducts(userId);
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}
