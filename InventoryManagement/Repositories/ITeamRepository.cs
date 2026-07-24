using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<TeamMember>> GetAllTeamMembersAsync();
        Task<TeamMember?> GetTeamMemberByIdAsync(int id);
        Task<int> CreateTeamMemberAsync(TeamMember member);
        Task<bool> UpdateTeamMemberAsync(int id, TeamMember member);
        Task<bool> UpdateTeamMemberRoleAsync(int id, string roleName);
        Task<bool> DeleteTeamMemberAsync(int id);
    }
}
