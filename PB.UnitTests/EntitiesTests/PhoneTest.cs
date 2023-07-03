using PB.Domain.Entities;
using PB.Domain.Enums;

namespace PB.UnitTests.EntitiesTests
{
    [Trait("Entities", "Phone")]
    public class PhoneTest
    {
        [Theory(DisplayName = "Valid phone entity creation")]
        [InlineData(EPhoneType.Mobile)]
        [InlineData(EPhoneType.Landline)]
        public void Phone_ValidEntity_ShouldReturnIsValidTrue(EPhoneType type)
        {
            var phone = new Phone("123", "123456789", type, Guid.NewGuid());

            Assert.True(phone.IsValid());
        }

        [Theory(DisplayName = "Invalid phone ddd")]
        [InlineData("")]
        [InlineData("1234")]
        [InlineData("12")]
        public void Phone_InvalidEntityDdd_ShouldReturnIsValidFalse(string ddd)
        {
            var phone = new Phone(ddd, "123456789", EPhoneType.Mobile, Guid.NewGuid());

            Assert.False(phone.IsValid());
        }

        [Fact(DisplayName = "Invalid phone number")]
        public void Phone_InvalidEntityEmail_ShouldReturnIsValidFalse()
        {
            var phone = new Phone("123", "", EPhoneType.Mobile, Guid.NewGuid());

            Assert.False(phone.IsValid());
        }

        [Fact(DisplayName = "Invalid phone type")]
        public void Phone_InvalidEntityType_ShouldReturnIsValidFalse()
        {
            var phone = new Phone("123", "123456789", (EPhoneType)2, Guid.NewGuid());

            Assert.False(phone.IsValid());
        }
    }
}