using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.UseCases.Addresses
{
    public class FindAddressByIdUseCase(IAddressRepository addressRepository)
    {
        private readonly IAddressRepository _addressRepository = addressRepository;

        public async Task<Address> GetAddressByIdAsync(Guid id)
        {
            var address = await _addressRepository.GetAddressByIdAsync(id);
            ValidateAddressExists(address);
            return address;
        }

        private void ValidateAddressExists(Address address)
        {
            if (address == null)
            {
                throw new KeyNotFoundException("Address not Found");
            }
        }
    }
}
