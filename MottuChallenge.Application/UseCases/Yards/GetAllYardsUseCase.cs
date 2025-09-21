using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Application.Interfaces;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Application.UseCases.Addresses;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class GetAllYardsUseCase
    {
        private readonly IYardRepository _yardRepository;
        private readonly FindAddressByIdUseCase _findAddressByIdUseCase;
        public GetAllYardsUseCase(IYardRepository yardRepository, FindAddressByIdUseCase findAddressByIdUseCase)
        {
            _yardRepository = yardRepository;
            _findAddressByIdUseCase = findAddressByIdUseCase;
        }

        public async Task<List<YardResponseDto>> FindAllYards()
        {
            var yards = await _yardRepository.GetAllYardsAsync();
            var result = new List<YardResponseDto>();

            foreach (var yard in yards)
            {
                var address = await _findAddressByIdUseCase.GetAddressByIdAsync(yard.AddressId);
                yard.Address = address;

                result.Add(new YardResponseDto
                {
                    Id = yard.Id,
                    Name = yard.Name,
                    Address = AddressMapping.CreateAddressResponseDto(address),
                    Points = PolygonPointsMapping.CreateListOfPointResponseDto(yard.Points)
                });
            }

            return result;
        }
    }
}
