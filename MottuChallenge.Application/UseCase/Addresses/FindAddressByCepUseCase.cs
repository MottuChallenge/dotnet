using MottuChallenge.Application.Interfaces;
using MottuChallenge.Domain.Entities;


namespace MottuChallenge.Application.UseCase.Addresses
{
    public class FindAddressByCepUseCase
    {
        private readonly IAddressProvider _addressProvider;

        public FindAddressByCepUseCase(IAddressProvider addressProvider)
        {
            _addressProvider = addressProvider;
        }

        public async Task<Address> FindAddressByCep(string cep, string number)
        {
            var address = await _addressProvider.GetAddressByCepAsync(cep, number);
            return address;
        }
    }
}
