using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Infrastructure.Mappings
{
    public class BranchMapping : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branchs");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.Phone)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(b => b.CNPJ)
                .IsRequired()
                .HasMaxLength(14);
            builder.HasMany(b => b.Yards)
                .WithOne(y => y.Branch)
                .HasForeignKey(y => y.BranchId);
            builder.HasOne(b => b.Address)
               .WithOne(a => a.Branch)
               .HasForeignKey<Branch>(b => b.AddressId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
