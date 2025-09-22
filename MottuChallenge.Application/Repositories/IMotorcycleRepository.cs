using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> SaveMotorcycleAsync(Motorcycle motorcycle);
        Task<Motorcycle> UpdateAsync(Motorcycle motorcycle);
    }
}
