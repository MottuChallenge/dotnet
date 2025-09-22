﻿using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Application.UseCases.Addresses;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class GetAllYardsUseCase
    {
        private readonly IYardRepository _yardRepository;
        public GetAllYardsUseCase(IYardRepository yardRepository)
        {
            _yardRepository = yardRepository;
        }

        public async Task<List<YardResponseDto>> FindAllYards()
        {
            var yards = await _yardRepository.GetAllYardsAsync();
            var result = new List<YardResponseDto>();

            foreach (var yard in yards)
            {

                result.Add(new YardResponseDto
                {
                    Id = yard.Id,
                    Name = yard.Name,
                    Address = AddressMapping.CreateAddressResponseDto(yard.Address),
                    Points = PolygonPointsMapping.CreateListOfPointResponseDto(yard.Points)
                });
            }

            return result;
        }
    }
}
