using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.Application.UseCases.Motorcycles
{
    public class DeleteMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public DeleteMotorcycleUseCase(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task DeleteMotorcycleAsync(Guid motorcycleId)
        {
            var motorcycle = await _motorcycleRepository.GetByIdAsync(motorcycleId);
            if (motorcycle == null)
                throw new KeyNotFoundException($"Motorcycle with ID {motorcycleId} not found.");

            // Se estiver associado a um Spot, você poderia limpar a associação aqui
            // motorcycle.RemoveSpot();

            await _motorcycleRepository.DeleteAsync(motorcycle);
        }
    }
}
