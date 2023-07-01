using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PB.Domain.Entities;
using PB.Domain.Enums;

namespace PB.Infra.Data.Mapping
{
    public class PhoneMapping : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder
                .ToTable("Phones");

            builder
                .Property(p => p.Ddd)
                .HasMaxLength(3);

            builder
                .Property(p => p.PhoneType)
                .HasConversion(new EnumToStringConverter<EPhoneType>());

            builder.HasQueryFilter(ce => ce.IsDeleted == false);

            builder.Ignore(ce => ce.ValidationResult);

            builder.Ignore(t => t.RuleLevelCascadeMode);
            builder.Ignore(t => t.CascadeMode);
            builder.Ignore(t => t.ClassLevelCascadeMode);
        }
    }
}