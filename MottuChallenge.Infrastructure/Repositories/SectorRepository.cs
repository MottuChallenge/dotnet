using Microsoft.EntityFrameworkCore;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class SectorRepository(MottuChallengeContext context) : ISectorRepository
    {

        private readonly MottuChallengeContext _context = context;

        public async Task<Sector> SaveSectorAsync(Sector sector)
        {
            await _context.Sectors.AddAsync(sector);
            await _context.SaveChangesAsync();
            return sector;
        }

        public async Task<List<Sector>> GetSectorsByYardIdAsync(Guid yardId)
        {
            return await _context.Sectors
                                 .Where(s => s.YardId == yardId)
                                 .ToListAsync();
        }

        public async Task<Sector> GetSectorByIdAsync(Guid sectorId)
        {
            return await _context.Sectors
                                 .Include(s => s.Spots)
                                 .FirstOrDefaultAsync(s => s.Id == sectorId);
        }

        public async Task<List<Sector>> GetAllSectorsAsync()
        {
            return await _context.Sectors
                                 .Include(s => s.Spots)
                                 .ToListAsync();
        }
    }
}
