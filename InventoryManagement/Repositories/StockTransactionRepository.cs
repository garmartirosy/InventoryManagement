using InventoryManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace InventoryManagement.Repositories
{
    public class StockTransactionRepository
    {
        private readonly string _connectionString;

        public StockTransactionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<StockTransaction>> GetStockTransactions(string userId)
        {
            using var conn = CreateConnection();

            var stockTransactions = await conn.QueryAsync<StockTransaction>(
                "GetStockTransactions",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return stockTransactions;
        }

        public async Task<StockTransaction?> GetStockTransactionById(int stockTransactionId, string userId)
        {
            using var conn = CreateConnection();

            var stockTransaction = await conn.QueryFirstOrDefaultAsync<StockTransaction>(
                "GetStockTransactionById",
                new { StockTransactionId = stockTransactionId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return stockTransaction;
        }

        public async Task CreateStockTransaction(StockTransaction stockTransaction, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "CreateStockTransaction",
                new
                {
                    UserId = userId,
                    stockTransaction.ProductId,
                    stockTransaction.Amount,
                    stockTransaction.Description
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task UpdateStockTransaction(StockTransaction stockTransaction, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "UpdateStockTransaction",
                new
                {
                    StockTransactionId = stockTransaction.Id,
                    UserId = userId,
                    stockTransaction.ProductId,
                    stockTransaction.Amount,
                    stockTransaction.Description
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteStockTransaction(int stockTransactionId, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "DeleteStockTransaction",
                new { StockTransactionId = stockTransactionId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}
