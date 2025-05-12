using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Infrastructure.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.Number)
                .IsRequired()
                .HasMaxLength(7);
            builder.Property(a => a.Neighborhood)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(a => a.ZipCode)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(a => a.Country)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
