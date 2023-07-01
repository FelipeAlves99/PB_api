using FluentValidation;
using PB.Domain.Shared.Entities;

namespace PB.Domain.Entities
{
    public sealed class Client : Entity<Client>
    {
        private IList<Phone> _phones;

        public Client(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            _phones = new List<Phone>();
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<Phone> Phones => _phones;

        public override bool IsValid()
        {
            ValidateFullName();
            ValidateEmail();

            AddErrors(Validate(this));

            return ValidationResult.IsValid;
        }

        private void ValidateEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("You must inform an email address")
                .EmailAddress().WithMessage("Email must be in a correct format");
        }

        private void ValidateFullName()
        {
            RuleFor(c => c.FullName)
                .NotEmpty().WithMessage("You must inform your full name")
                .Must(c => c.Split(' ').Length > 1).WithMessage("You must inform at least your first and last name");

        }

        internal void Update(string email)
        {
            Email = email;
            UpdatedOn = DateTime.Now;
        }
    }
}
