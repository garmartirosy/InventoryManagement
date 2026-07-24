using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeamMembers()
        {
            var members = await _teamRepository.GetAllTeamMembersAsync();
            return Ok(members);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamMemberById(int id)
        {
            var member = await _teamRepository.GetTeamMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeamMember([FromBody] TeamMember member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newId = await _teamRepository.CreateTeamMemberAsync(member);

            return CreatedAtAction(
                nameof(GetTeamMemberById),
                new { id = newId },
                member);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMember(int id, [FromBody] TeamMember member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _teamRepository.UpdateTeamMemberAsync(id, member);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}/role")]
        public async Task<IActionResult> UpdateTeamMemberRole(int id, [FromBody] UpdateTeamMemberRoleRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _teamRepository.UpdateTeamMemberRoleAsync(id, request.RoleName);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamMember(int id)
        {
            var deleted = await _teamRepository.DeleteTeamMemberAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

    public class UpdateTeamMemberRoleRequest
    {
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;
    }
}