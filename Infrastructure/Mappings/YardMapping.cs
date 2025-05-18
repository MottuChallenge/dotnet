using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Infrastructure.Mappings
{
    public class YardMapping : IEntityTypeConfiguration<Yard>
    {
        public void Configure(EntityTypeBuilder<Yard> builder)
        { 
            builder.ToTable("Yards");
            builder.HasKey(y => y.Id);
            builder.Property(y => y.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(y => y.Area)
                .IsRequired()
                .HasPrecision(10, 2);
            builder.HasOne(y => y.Branch)
                .WithMany(b => b.Yards)
                .HasForeignKey(y => y.BranchId);
            builder.HasOne(y => y.Address)
               .WithOne(a => a.Yard)
               .HasForeignKey<Yard>(y => y.AddressId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
