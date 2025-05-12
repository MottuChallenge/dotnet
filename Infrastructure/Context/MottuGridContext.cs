using Microsoft.EntityFrameworkCore;
using MottuGrid_Dotnet.Domain.Entities;
using MottuGrid_Dotnet.Infrastructure.Mappings;

namespace MottuGrid_Dotnet.Infrastructure.Context
{
    public class MottuGridContext(DbContextOptions<MottuGridContext> options) : DbContext(options)
    {
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<Section> sections { get; set; }
        public DbSet<Yard> yards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MotorcycleMapping());
            modelBuilder.ApplyConfiguration(new BranchMapping());
            modelBuilder.ApplyConfiguration(new AddressMapping());
            modelBuilder.ApplyConfiguration(new LogMapping());
            modelBuilder.ApplyConfiguration(new SectionMapping());
            modelBuilder.ApplyConfiguration(new YardMapping());
        }
    }
}
