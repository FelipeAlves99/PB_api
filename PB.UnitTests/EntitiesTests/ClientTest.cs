using PB.Domain.Entities;

namespace PB.UnitTests.EntitiesTests
{
    [Trait("Entities", "Client")]
    public class ClientTest
    {
        [Fact(DisplayName = "Valid client entity creation")]
        public void Client_ValidEntity_ShouldReturnIsValidTrue()
        {
            var client = new Client("Valid Test", "valid@email.com");

            Assert.True(client.IsValid());
        }

        [Theory(DisplayName = "Invalid client name")]
        [InlineData("")]
        [InlineData("InvalidName")]
        public void Client_InvalidEntityName_ShouldReturnIsValidFalse(string name)
        {
            var client = new Client(name, "valid@email.com");

            Assert.False(client.IsValid());
        }

        [Theory(DisplayName = "Invalid client email")]
        [InlineData("")]
        [InlineData("invalidEmail")]
        public void Client_InvalidEntityEmail_ShouldReturnIsValidFalse(string email)
        {
            var client = new Client("Single Name", email);

            Assert.False(client.IsValid());
        }
    }
}
