using Dapper;
using InventoryManagement.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly string _connectionString;

        public TeamRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync()
        {
            using var db = CreateConnection();

            return await db.QueryAsync<TeamMember>(
                "GetTeamMembers",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<TeamMember?> GetTeamMemberByIdAsync(int id)
        {
            using var db = CreateConnection();

            return await db.QueryFirstOrDefaultAsync<TeamMember>(
                "GetTeamMemberById",
                new { TeamMemberId = id },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateTeamMemberAsync(TeamMember member)
        {
            using var db = CreateConnection();

            return await db.ExecuteScalarAsync<int>(
                "CreateTeamMember",
                new
                {
                    member.FullName,
                    member.Email,
                    member.RoleName,
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> UpdateTeamMemberAsync(int id, TeamMember member)
        {
            using var db = CreateConnection();

            var rowsAffected = await db.ExecuteAsync(
                "UpdateTeamMember",
                new
                {
                    TeamMemberId = id,
                    member.FullName,
                    member.Email,
                    member.RoleName
                },
                commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }

        public async Task<bool> UpdateTeamMemberRoleAsync(int id, string roleName)
        {
            using var db = CreateConnection();

            var rowsAffected = await db.ExecuteAsync(
                "UpdateTeamMemberRole",
                new { TeamMemberId = id, RoleName = roleName },
                commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }

        public async Task<bool> DeleteTeamMemberAsync(int id)
        {
            using var db = CreateConnection();

            var rowsAffected = await db.ExecuteAsync(
                "DeleteTeamMember",
                new { TeamMemberId = id },
                commandType: CommandType.StoredProcedure);

            return rowsAffected > 0;
        }
    }
}