using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Infrastructure.Mappings
{
    public class MotorcycleMapping : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.ToTable("Motorcycles");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Model)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(m => m.EngineType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);
            builder.Property(m => m.Plate)
                .IsRequired()
                .HasMaxLength(10);
            builder.Property(m => m.LastRevisionDate)
                .IsRequired();
            builder.HasOne(m => m.Section)
                   .WithMany(s => s.Motorcycles)
                   .HasForeignKey(m => m.SectionId);
        }
    }
}
