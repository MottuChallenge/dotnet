using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface ISectorRepository
    {

        Task<Sector> SaveSectorAsync(Sector sector);

        Task<List<Sector>> GetSectorsByYardIdAsync(Guid yardId);

        Task<Sector> GetSectorByIdAsync(Guid sectorId);
        Task<List<Sector>> GetAllSectorsAsync();
    }
}
