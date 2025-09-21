using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Infrastructure.Repositories
{
    public interface IAddressRepository
    {

        Task<Address> GetAddressByIdAsync(Guid id);
    }
}
