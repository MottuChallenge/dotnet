using MottuChallenge.Application.Repositories;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class GetYardEntityByIdUseCase
    {
        private readonly IYardRepository _yardRepository;
        private readonly FindAddressByIdUseCase _findAddressByIdUseCase;

        public GetYardEntityByIdUseCase(IYardRepository yardRepository, FindAddressByIdUseCase findAddressByIdUseCase)
        {
            _yardRepository = yardRepository;
            _findAddressByIdUseCase = findAddressByIdUseCase;
        }

        public async Task<Yard> FindYardById(Guid id)
        {
            var yard = await _yardRepository.GetYardByIdAsync(id);
            ValidateYardExists(yard);
            var address = await _findAddressByIdUseCase.GetAddressByIdAsync(yard.AddressId);
            yard.Address = address;

            return yard;
        }

        private void ValidateYardExists(Yard yard)
        {
            if (yard == null)
            {
                throw new KeyNotFoundException("Yard not Found");
            }
        }
    }
}
