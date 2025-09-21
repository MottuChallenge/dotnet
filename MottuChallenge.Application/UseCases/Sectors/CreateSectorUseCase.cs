using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Helpers;
using MottuChallenge.Application.UseCases.Spots;
using MottuChallenge.Application.UseCases.Yards;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.Sectors
{
    public class CreateSectorUseCase
    {
        private readonly ISectorRepository _sectorRepository;
        private readonly GetYardEntityByIdUseCase _getYardEntityByIdUseCase;
        private readonly GenerateSpotsUseCase _generateSpotsUseCase;

        public CreateSectorUseCase(ISectorRepository sectorRepository, GetYardEntityByIdUseCase getYardEntityByIdUseCase, GenerateSpotsUseCase generateSpotsUseCase)
        {
            _sectorRepository = sectorRepository;
            _getYardEntityByIdUseCase = getYardEntityByIdUseCase;
            _generateSpotsUseCase = generateSpotsUseCase;
        }

        public async Task<Sector> SaveSector(SectorCreateDto sectorCreateDto)
        {
            var sector = new Sector(sectorCreateDto.SectorTypeId, sectorCreateDto.YardId);

            var yard = await _getYardEntityByIdUseCase.FindYardById(sector.YardId);

            ValidateYardExists(yard);
            ValidateSectorInsideYard(sector, yard);

            var existingSectors = await _sectorRepository.GetSectorsByYardIdAsync(yard.Id);
            ValidateSectorOverlap(sector, existingSectors);

            foreach (var point in sectorCreateDto.Points)
                sector.AddPoint(new PolygonPoint(point.PointOrder, point.X, point.Y));

            var spots = _generateSpotsUseCase.GenerateSpot(sector, 2, 2);
            foreach (var spot in spots)
                sector.AddSpot(spot);

            return await _sectorRepository.SaveSectorAsync(sector);
        }

        private void ValidateSectorInsideYard(Sector sector, Yard yard)
        {
            bool isInside = sector.Points.All(p => GeometryHelper.IsPointInsidePolygon(p.X, p.Y, yard.Points.ToList()));
            if (!isInside)
            {
                throw new InvalidOperationException("Sector is not fully inside the Yard.");
            }
        }

        private void ValidateSectorOverlap(Sector sector, List<Sector> existingSectors)
        {
            foreach (var existingSector in existingSectors)
            {
                bool overlap = sector.Points.Any(p =>
                    GeometryHelper.IsPointInsidePolygon(p.X, p.Y, existingSector.Points.ToList()));
                if (overlap)
                {
                    throw new InvalidOperationException("Sector overlaps with another existing sector.");
                }
            }
        }

        private void ValidateYardExists(Yard yard)
        {
            if (yard == null)
            {
                throw new InvalidOperationException("Yard does not exist.");
            }
        }
    }
}
