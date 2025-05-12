using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MottuGrid_Dotnet.Domain.Entities;

namespace MottuGrid_Dotnet.Infrastructure.Mappings
{
    public class LogMapping : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Message)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(l => l.CreatedAt)
                .IsRequired();
            builder.HasOne(l => l.Motorcycle)
                   .WithMany(m => m.Logs)
                   .HasForeignKey(l => l.MotorcycleId);
        }
    }
}
