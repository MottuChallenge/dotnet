using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCase.Addresses
{
    public class FindAddressByIdUseCase(IAddressRepository addressRepository)
    {
        private readonly IAddressRepository _addressRepository = addressRepository;

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            return await _addressRepository.GetAddressByIdAsync(id);
        }
    }
}
