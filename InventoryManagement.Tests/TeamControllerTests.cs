using InventoryManagement.Controllers;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InventoryManagement.Tests
{
    public class TeamControllerTests
    {
        [Fact]
        public async Task GetAllTest()
        {
            var mockRepo = new Mock<ITeamRepository>();
            var members = new List<TeamMember>
            {
                new TeamMember { Id = 1, FullName = "John", Email = "john@test.com", RoleName = "Admin" }
            };
            mockRepo.Setup(r => r.GetAllTeamMembersAsync()).ReturnsAsync(members);

            var controller = new TeamController(mockRepo.Object);
            var result = await controller.GetAllTeamMembers();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(members, ok.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenMissing()
        {
            var mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(r => r.GetTeamMemberByIdAsync(99)).ReturnsAsync((TeamMember?)null);

            var controller = new TeamController(mockRepo.Object);
            var result = await controller.GetTeamMemberById(99);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenValid()
        {
            var mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(r => r.CreateTeamMemberAsync(It.IsAny<TeamMember>())).ReturnsAsync(5);

            var controller = new TeamController(mockRepo.Object);
            var member = new TeamMember { FullName = "Jane", Email = "jane@test.com", RoleName = "User" };
            var result = await controller.CreateTeamMember(member);

            Assert.IsType<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenUpdated()
        {
            var mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(r => r.UpdateTeamMemberAsync(1, It.IsAny<TeamMember>())).ReturnsAsync(true);

            var controller = new TeamController(mockRepo.Object);
            var member = new TeamMember { FullName = "Jane", Email = "jane@test.com", RoleName = "User" };
            var result = await controller.UpdateTeamMember(1, member);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateRole_ReturnsNoContent_WhenUpdated()
        {
            var mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(r => r.UpdateTeamMemberRoleAsync(1, "Admin")).ReturnsAsync(true);

            var controller = new TeamController(mockRepo.Object);
            var request = new UpdateTeamMemberRoleRequest { RoleName = "Admin" };
            var result = await controller.UpdateTeamMemberRole(1, request);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenMissing()
        {
            var mockRepo = new Mock<ITeamRepository>();
            mockRepo.Setup(r => r.DeleteTeamMemberAsync(99)).ReturnsAsync(false);

            var controller = new TeamController(mockRepo.Object);
            var result = await controller.DeleteTeamMember(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
