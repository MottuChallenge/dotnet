using MottuChallenge.Application.DTOs.Request;
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

        public async Task SaveMotorcycleAsync(CreateMotorcycleDto motorcycleDto)
        {
            var motorcycle = new Motorcycle(motorcycleDto.Model, motorcycleDto.EngineType, motorcycleDto.Plate, motorcycleDto.LastRevisionDate);
            await _motorcycleRepository.SaveMotorcycleAsync(motorcycle);

            if (motorcycleDto.SpotId != Guid.Empty)
            {
                var sector = await _sectorRepository.GetSectorBySpotId(motorcycleDto.SpotId);
            
                sector.AssignMotorcycleToSpot(motorcycleDto.SpotId, motorcycle);
                await _sectorRepository.UpdateAsync(sector);
                await _motorcycleRepository.UpdateAsync(motorcycle);
            }
        }
    }
}
