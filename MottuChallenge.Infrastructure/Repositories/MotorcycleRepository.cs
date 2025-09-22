using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly MottuChallengeContext _context;

        public MotorcycleRepository(MottuChallengeContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> SaveMotorcycleAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.AddAsync(motorcycle);
            await _context.SaveChangesAsync();
            return motorcycle;
        }

        public async Task<Motorcycle> UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            await _context.SaveChangesAsync();
            return motorcycle;
        }
    }
}
