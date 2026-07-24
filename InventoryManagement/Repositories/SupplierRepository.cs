using InventoryManagement.Models;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace InventoryManagement.Repositories
{
    public class SupplierRepository
    {
        private readonly string _connectionString;

        public SupplierRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Supplier>> GetSuppliers(string userId)
        {
            using var conn = CreateConnection();

            var suppliers = await conn.QueryAsync<Supplier>(
                "GetSuppliers",
                new { UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return suppliers;
        }

        public async Task<Supplier?> GetSupplierById(int supplierId, string userId)
        {
            using var conn = CreateConnection();

            var supplier = await conn.QueryFirstOrDefaultAsync<Supplier>(
                "GetSupplierById",
                new { SupplierId = supplierId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );

            return supplier;
        }

        public async Task CreateSupplier(Supplier supplier, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "CreateSupplier",
                new
                {
                    UserId = userId,
                    supplier.Name
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task UpdateSupplier(Supplier supplier, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "UpdateSupplier",
                new
                {
                    SupplierId = supplier.Id,
                    UserId = userId,
                    supplier.Name
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task DeleteSupplier(int supplierId, string userId)
        {
            using var conn = CreateConnection();

            await conn.ExecuteAsync(
                "DeleteSupplier",
                new { SupplierId = supplierId, UserId = userId },
                commandType: CommandType.StoredProcedure
            );
        }

    }
}
