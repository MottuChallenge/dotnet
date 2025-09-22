using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Repositories
{
    public interface ISectorRepository
    {

        Task<Sector> SaveSectorAsync(Sector sector);
        Task<List<Sector>> GetSectorsByYardIdAsync(Guid yardId);
        Task<Sector> GetSectorByIdAsync(Guid sectorId);
        Task<List<Sector>> GetAllSectorsAsync();
        Task<Sector> GetSectorBySpotId(Guid spotId);
        Task<Sector> UpdateAsync(Sector sector);
    }
}
