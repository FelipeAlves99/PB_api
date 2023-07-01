using Microsoft.EntityFrameworkCore;
using PB.Domain.Entities;
using PB.Infra.Data.Mapping;

namespace PB.Infra.Data.Context
{
    public class PbContext : DbContext
    {
        public PbContext() { }

        public PbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Phone> Phones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientMapping());
            modelBuilder.ApplyConfiguration(new PhoneMapping());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=localhost,1433;database=PB; User ID=SA;Password=1q2w3e4r!@#$");
        }
    }
}