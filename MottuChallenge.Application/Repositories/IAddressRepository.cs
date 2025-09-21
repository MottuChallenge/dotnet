using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Repositories
{
    public interface IAddressRepository
    {

        Task<Address> GetAddressByIdAsync(Guid id);
    }
}
