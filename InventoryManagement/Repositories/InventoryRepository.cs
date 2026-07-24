using InventoryManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace InventoryManagement.Repositories
{
    public class InventoryRepository
    {
        private readonly string _connectionString;

        public InventoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Inventory>> GetInventories(string userId)
        {
            using var conn = CreateConnection();

            var inventories = await conn.QueryAsync<Inventory>(
                "GetInventories",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return inventories;
        }

        public async Task<Inventory?> GetInventoryById(int inventoryId, string userId)
        {
            using var conn = CreateConnection();

            var inventory = await conn.QueryFirstOrDefaultAsync<Inventory>(
                "GetInventoryById",
                new { InventoryId = inventoryId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return inventory;
        }

        public async Task CreateInventory(Inventory inventory, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "CreateInventory",
                new
                {
                    UserId = userId,
                    inventory.ProductId,
                    inventory.Amount
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task UpdateInventory(Inventory inventory, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "UpdateInventory",
                new
                {
                    InventoryId = inventory.Id,
                    UserId = userId,
                    inventory.ProductId,
                    inventory.Amount
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteInventory(int inventoryId, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "DeleteInventory",
                new { InventoryId = inventoryId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}
