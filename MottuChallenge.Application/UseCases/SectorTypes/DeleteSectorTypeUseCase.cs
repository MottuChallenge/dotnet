using MottuChallenge.Application.Interfaces;
using MottuChallenge.Infrastructure.Repositories;

namespace MottuChallenge.Application.UseCases.SectorTypes
{
    public class DeleteSectorTypeUseCase
    {
        private readonly ISectorTypeRepository _repository;

        public DeleteSectorTypeUseCase(ISectorTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task DeleteSectorTypeById(Guid id)
        {
            var sectorType = await _repository.FindAsync(id);
            if (sectorType == null)
                throw new KeyNotFoundException($"SectorType with id {id} not found.");

            await _repository.DeleteSectorTypeAsync(sectorType);
        }
    }
}
