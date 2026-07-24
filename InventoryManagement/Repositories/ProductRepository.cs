using InventoryManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace InventoryManagement.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Product>> GetProducts(string userId)
        {
            using var conn = CreateConnection();

            var products = await conn.QueryAsync<Product>(
                "GetProducts",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return products;
        }

        public async Task<Product?> GetProductById(int productId, string userId)
        {
            using var conn = CreateConnection();

            var product = await conn.QueryFirstOrDefaultAsync<Product>(
                "GetProductById",
                new { ProductId = productId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return product;
        }

        public async Task CreateProduct(Product product, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "CreateProduct",
                new
                {
                    UserId = userId,
                    product.Name,
                    product.Price,
                    product.CategoryId,
                    product.SupplierId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task UpdateProduct(Product product, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "UpdateProduct",
                new
                {
                    ProductId = product.Id,
                    UserId = userId,
                    product.Name,
                    product.Price,
                    product.CategoryId,
                    product.SupplierId
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteProduct(int productId, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "DeleteProduct",
                new { ProductId = productId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}
