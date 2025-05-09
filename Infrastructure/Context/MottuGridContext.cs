using Microsoft.EntityFrameworkCore;

namespace MottuGrid_Dotnet.Infrastructure.Context
{
    public class MottuGridContext(DbContextOptions<MottuGridContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
        }
    }
}
