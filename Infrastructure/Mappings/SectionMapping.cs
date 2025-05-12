using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Infrastructure.Mappings
{
    public class SectionMapping : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.ToTable("Sections");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Color)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(s => s.Area)
                .IsRequired()
                .HasPrecision(10, 2);
            builder.HasMany(s => s.Motorcycles)
                   .WithOne(m => m.Section)
                   .HasForeignKey(m => m.SectionId);
        }
    }
    
}
