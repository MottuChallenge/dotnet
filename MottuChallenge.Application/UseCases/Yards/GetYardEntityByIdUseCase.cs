using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Infrastructure.Repositories;

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
            var address = await _findAddressByIdUseCase.GetAddressByIdAsync(yard.AddressId);
            yard.Address = address;

            return yard;
        }
    }
}
