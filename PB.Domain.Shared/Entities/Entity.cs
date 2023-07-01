using FluentValidation;
using FluentValidation.Results;

namespace PB.Domain.Shared.Entities
{
    public abstract class Entity<T> : AbstractValidator<T>
        where T : Entity<T>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            ValidationResult = new();
        }

        public Guid Id { get; protected set; }

        public bool IsDeleted { get; protected set; }

        public DateTime CreatedOn { get; protected set; }

        public DateTime UpdatedOn { get; protected set; }

        public ValidationResult ValidationResult { get; protected set; }

        public void SetIsDeleted()
        {
            IsDeleted = !IsDeleted;
            UpdatedOn = DateTime.Now;
        }

        public abstract bool IsValid();

        protected void AddErrors(ValidationResult validateResult)
        {
            foreach (var error in validateResult.Errors)
            {
                ValidationResult.Errors.Add(error);
            }
        }
    }
}