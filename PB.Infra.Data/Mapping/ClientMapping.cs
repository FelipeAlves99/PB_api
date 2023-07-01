using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PB.Domain.Entities;

namespace PB.Infra.Data.Mapping
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(c => c.Phones)
                .WithOne(p => p.Client)
                .HasForeignKey(c => c.ClientId)
                .IsRequired();

            builder
                .ToTable("Clients");

            builder.HasQueryFilter(ce => ce.IsDeleted == false);

            builder.Ignore(ce => ce.ValidationResult);

            builder.Ignore(t => t.RuleLevelCascadeMode);
            builder.Ignore(t => t.CascadeMode);
            builder.Ignore(t => t.ClassLevelCascadeMode);
        }
    }
}