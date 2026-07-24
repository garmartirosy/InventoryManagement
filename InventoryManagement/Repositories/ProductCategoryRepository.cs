using InventoryManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Microsoft.Extensions.Caching.Memory;

namespace InventoryManagement.Repositories
{
    public class ProductCategoryRepository
    {
        private readonly string _connectionString;
        private readonly IMemoryCache _cache;

        public ProductCategoryRepository(IConfiguration configuration, IMemoryCache cache)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _cache = cache;
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<ProductCategory>> GetProductCategories(string userId)
        {
            var cacheKey = $"categories:{userId}";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductCategory> cachedCategories))
            {
                return cachedCategories;
            }

            using var connection = CreateConnection();

            var categories = await connection.QueryAsync<ProductCategory>(
                "GetProductCategories",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure);

            _cache.Set(cacheKey, categories, TimeSpan.FromMinutes(5));

            return categories;
        }

        public async Task<ProductCategory?> GetProductCategoryById(int productCategoryId, string userId)
        {
            using var conn = CreateConnection();

            var productCategory = await conn.QueryFirstOrDefaultAsync<ProductCategory>(
                "GetProductCategoryById",
                new { ProductCategoryId = productCategoryId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return productCategory;
        }

        public async Task CreateProductCategory(ProductCategory productCategory, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "CreateProductCategory",
                new
                {
                    UserId = userId,
                    productCategory.Name
                },
                commandType: CommandType.StoredProcedure
            );

            _cache.Remove($"categories:{userId}");
        }

        public async Task UpdateProductCategory(ProductCategory productCategory, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "UpdateProductCategory",
                new
                {
                    ProductCategoryId = productCategory.Id,
                    UserId = userId,
                    productCategory.Name
                },
                commandType: CommandType.StoredProcedure
            );

            _cache.Remove($"categories:{userId}");
        }

        public async Task DeleteProductCategory(int productCategoryId, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "DeleteProductCategory",
                new { ProductCategoryId = productCategoryId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            _cache.Remove($"categories:{userId}");

        }

    }
}
