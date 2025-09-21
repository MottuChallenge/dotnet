using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Repositories
{
    public interface IYardRepository
    {
        Task<Yard> SaveYardAsync(Yard yard);

        Task<Yard?> GetYardByIdAsync(Guid id);

        Task<List<Yard>> GetAllYardsAsync();

    }
}
