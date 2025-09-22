using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Application.Repositories;

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
            ValidateYardExists(yard);
            var address = await _findAddressByIdUseCase.GetAddressByIdAsync(yard.AddressId);
            yard.SetAddress(address);

            return new YardResponseDto
            {
                Id = yard.Id,
                Name = yard.Name,
                Address = AddressMapping.CreateAddressResponseDto(address),
                Points = PolygonPointsMapping.CreateListOfPointResponseDto(yard.Points)
            };
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
