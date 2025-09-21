using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Application.Interfaces;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class GetYardByIdUseCase
    {
        private readonly IYardRepository _yardRepository;
        private readonly FindAddressByIdUseCase _findAddressByIdUseCase;

        public GetYardByIdUseCase(IYardRepository yardRepository, FindAddressByIdUseCase findAddressByIdUseCase)
        {
            _yardRepository = yardRepository;
            _findAddressByIdUseCase = findAddressByIdUseCase;
        }

        public async Task<YardResponseDto> FindYardById(Guid id)
        {
            var yard = await _yardRepository.GetYardByIdAsync(id);
            var address = await _findAddressByIdUseCase.GetAddressByIdAsync(yard.AddressId);
            yard.Address = address;

            return new YardResponseDto
            {
                Id = yard.Id,
                Name = yard.Name,
                Address = AddressMapping.CreateAddressResponseDto(address),
                Points = PolygonPointsMapping.CreateListOfPointResponseDto(yard)
            };
        }
    }
}
