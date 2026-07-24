using System.ComponentModel.DataAnnotations;
using InventoryManagement.Models;

namespace InventoryManagement.Tests
{
    public class ProductValidationTests
    {
        [Fact]
        public void Name_Required_FailsWhenEmpty()
        {
            var product = new Product { Name = "", Price = 10 };

            var results = Validate(product);

            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void TestLongName()
        {
            var product = new Product { Name = new string('a', 101), Price = 10 };

            var results = Validate(product);

            Assert.Contains(results, r => r.MemberNames.Contains("Name"));
        }

        [Fact]
        public void Price_MustBeGreaterThanZero()
        {
            var product = new Product { Name = "Widget", Price = 0 };

            var results = Validate(product);

            Assert.Contains(results, r => r.MemberNames.Contains("Price"));
        }

        [Fact]
        public void ValidProduct_PassesValidation()
        {
            var product = new Product { Name = "Widget", Price = 10 };

            var results = Validate(product);

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
