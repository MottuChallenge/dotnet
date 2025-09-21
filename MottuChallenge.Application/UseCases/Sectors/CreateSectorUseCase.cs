using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Application.UseCases.SectorTypes;
using MottuChallenge.Application.UseCases.Spots;
using MottuChallenge.Application.UseCases.Yards;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;

namespace MottuChallenge.Application.UseCases.Sectors
{
    public class CreateSectorUseCase
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly GetYardEntityByIdUseCase _getYardEntityByIdUseCase;
        private readonly GenerateSpotsUseCase _generateSpotsUseCase;
        private readonly GetSectorTypeByIdUseCase _getSectorTypeByIdUseCase;

        public CreateSectorUseCase(ISectorRepository sectorRepository, GetYardEntityByIdUseCase getYardEntityByIdUseCase, GenerateSpotsUseCase generateSpotsUseCase, GetSectorTypeByIdUseCase getSectorTypeByIdUseCase)
        {
            _sectorRepository = sectorRepository;
            _getYardEntityByIdUseCase = getYardEntityByIdUseCase;
            _generateSpotsUseCase = generateSpotsUseCase;
            _getSectorTypeByIdUseCase = getSectorTypeByIdUseCase;
        }

        public async Task<Sector> SaveSector(SectorCreateDto sectorCreateDto)
        {
            var sector = new Sector();

            var yard = await _getYardEntityByIdUseCase.FindYardById(sector.YardId);

            var sectorType = await _getSectorTypeByIdUseCase.FindSectorTypeById(sectorCreateDto.SectorTypeId);

            sector.AddYard(yard);
            sector.AddSectorType(sectorType);
            sector.ValidateInsideYard();
            sector.AddPoints(sectorCreateDto.Points.Select(p => new PolygonPoint(p.PointOrder, p.X, p.Y)));

            var existingSectors = await _sectorRepository.GetSectorsByYardIdAsync(yard.Id);
            sector.ValidateOverlap(existingSectors);

            var spots = _generateSpotsUseCase.GenerateSpot(sector, 2, 2);

            sector.AddSpots(spots);

            return await _sectorRepository.SaveSectorAsync(sector);
        }

    }
}
