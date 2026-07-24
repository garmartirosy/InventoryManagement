using System.ComponentModel.DataAnnotations;
using InventoryManagement.Models;

namespace InventoryManagement.Tests
{
    public class TeamMemberValidationTests
    {
        [Fact]
        public void Email_InvalidFormat_FailsValidation()
        {
            var member = new TeamMember { FullName = "A B", Email = "----", RoleName = "Manager" };

            var results = Validate(member);

            Assert.Contains(results, r => r.MemberNames.Contains("Email"));
        }

        [Fact]
        public void FullName_Required_FailsWhenEmpty()
        {
            var member = new TeamMember { FullName = "", Email = "a@example.com", RoleName = "Manager" };

            var results = Validate(member);

            Assert.Contains(results, r => r.MemberNames.Contains("FullName"));
        }

        [Fact]
        public void TestValidMember()
        {
            var member = new TeamMember { FullName = "A B", Email = "ab@example.com", RoleName = "Manager" };

            var results = Validate(member);

            Assert.Empty(results);
        }

        private static List<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }
    }
}
