﻿using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Application.Repositories;

namespace MottuChallenge.Application.UseCases.Sectors
{
    public class GetSectorByIdUseCase
    {
        private readonly ISectorRepository _sectorRepository;

        public GetSectorByIdUseCase(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<SectorResponseDto> FindSectorById(Guid sectorId)
        {
            var sector = await _sectorRepository.GetSectorByIdAsync(sectorId);

            return new SectorResponseDto
            {
                Id = sector.Id,
                YardId = sector.YardId,
                SectorTypeId = sector.SectorTypeId,
                Points = PolygonPointsMapping.CreateListOfPointResponseDto(sector.Points),
                Spots = sector.Spots.Select(s => new SpotResponseDto { SpotId = s.SpotId, SectorId = s.SectorId, Status = s.Status, MotorcycleId = s.MotorcycleId, X = s.X, Y = s.Y }).ToList()
            };
        }
    }
}
