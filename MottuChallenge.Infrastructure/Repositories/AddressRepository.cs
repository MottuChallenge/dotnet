using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Persistence;

namespace MottuChallenge.Infrastructure.Repositories
{
    internal class AddressRepository(MottuChallengeContext context) : IAddressRepository
    {
        private readonly MottuChallengeContext _context = context;

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }
    }
}
