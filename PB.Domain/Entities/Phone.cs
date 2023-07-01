using FluentValidation;
using PB.Domain.Enums;
using PB.Domain.Shared.Entities;

namespace PB.Domain.Entities
{
    public sealed class Phone : Entity<Phone>
    {
        public Phone(string ddd, string phoneNumber, EPhoneType phoneType, Guid clientId)
        {
            Ddd = ddd;
            PhoneNumber = phoneNumber;
            PhoneType = phoneType;
            ClientId = clientId;
        }

        public string Ddd { get; private set; }
        public string PhoneNumber { get; private set; }
        public EPhoneType PhoneType { get; private set; }
        public Client Client { get; private set; }
        public Guid ClientId { get; private set; }

        public override bool IsValid()
        {
            ValidateDDD();
            ValidatePhoneNumber();
            ValidatePhoneType();
            ValidateClientId();

            AddErrors(Validate(this));

            return ValidationResult.IsValid;
        }

        private void ValidateClientId()
        {
            RuleFor(p => p.ClientId)
                .NotEmpty().WithMessage("You must inform the client");
        }

        private void ValidatePhoneType()
        {
            RuleFor(p => p.PhoneType)
                .IsInEnum().WithMessage("Phone type must be a valid value");
        }

        private void ValidatePhoneNumber()
        {
            RuleFor(p => p.PhoneNumber)
                .NotNull().WithMessage("You must inform your phone number");
        }

        private void ValidateDDD()
        {
            RuleFor(p => p.Ddd)
                .NotNull().WithMessage("You must inform thee phone region number")
                .Length(3).WithMessage("The phone region number must be 3 chars long");
        }

        internal void Update(string ddd, string phoneNumber, EPhoneType phoneType)
        {
            Ddd = ddd;
            PhoneNumber = phoneNumber;
            PhoneType = phoneType;
            UpdatedOn = DateTime.Now;
        }
    }
}