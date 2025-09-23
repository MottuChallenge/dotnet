using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Entities;
namespace MottuChallenge.Application.UseCases.Motorcycles
{
    public class CreateMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly ISectorRepository _sectorRepository;

        public CreateMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, ISectorRepository sectorRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _sectorRepository = sectorRepository;
        }

        public async Task<MotorcycleResponseDto> SaveMotorcycleAsync(MotorcycleDto motorcycleDto)
        {
            var motorcycle = new Motorcycle(motorcycleDto.Model, motorcycleDto.EngineType, motorcycleDto.Plate, motorcycleDto.LastRevisionDate);
            await _motorcycleRepository.SaveMotorcycleAsync(motorcycle);

            if (motorcycleDto.SpotId.HasValue && motorcycleDto.SpotId.Value != Guid.Empty)
            {
                var sector = await _sectorRepository.GetSectorBySpotId(motorcycleDto.SpotId.Value);

                ValidateIfExistMotorcycleInsideSpot(sector, motorcycleDto.SpotId.Value);
            
                sector.AssignMotorcycleToSpot(motorcycleDto.SpotId.Value, motorcycle);
                await _sectorRepository.UpdateAsync(sector);
                await _motorcycleRepository.UpdateAsync(motorcycle);
            }

            return new MotorcycleResponseDto
            {
                Id = motorcycle.Id,
                Model = motorcycle.Model,
                EngineType = motorcycle.EngineType,
                Plate = motorcycle.Plate,
                LastRevisionDate = motorcycle.LastRevisionDate,
                SpotId = motorcycle.SpotId
            }
            ;
        }

        private void ValidateIfExistMotorcycleInsideSpot(Sector sector, Guid spotId)
        {
            var spot = sector.Spots.FirstOrDefault(spot => spot.SpotId == spotId);
            if (spot == null)
            {
                throw new KeyNotFoundException("Spot not found");
            }

            if(spot.Status.Equals(Domain.Enums.SpotStatus.OCCUPIED))
            {
                throw new ArgumentException("This Spot is already occupied");
            }

        }
    }
}
