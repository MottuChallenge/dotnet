using Microsoft.EntityFrameworkCore;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class YardRepository(MottuChallengeContext context) : IYardRepository
    {
        private readonly MottuChallengeContext _context = context;

        public async Task<Yard> SaveYardAsync(Yard yard)
        {
           await _context.Yards.AddAsync(yard);
           await _context.SaveChangesAsync();
           return yard;
        }

        public async Task<Yard?> GetYardByIdAsync(Guid id)
        {
            return await _context.Yards.FindAsync(id);
        }

        public async Task<List<Yard>> GetAllYardsAsync()
        {
            return await _context.Yards.ToListAsync();
        }
    }
}
