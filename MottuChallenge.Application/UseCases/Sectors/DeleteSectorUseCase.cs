using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.Application.UseCases.Sectors
{
    public class DeleteSectorUseCase
    {
        private readonly ISectorRepository _sectorRepository;

        public DeleteSectorUseCase(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task DeleteSectorAsync(Guid sectorId)
        {
            var sector = await _sectorRepository.GetSectorByIdAsync(sectorId);
            if (sector == null)
                throw new KeyNotFoundException($"Sector with ID {sectorId} not found.");

            // Exemplo de regra de negócio: não deletar se tiver spots ocupados
            // if (sector.Spots.Any(s => s.Motorcycle != null))
            //     throw new DomainValidationException("Cannot delete sector with motorcycles assigned.");

            await _sectorRepository.DeleteAsync(sector);
        }
    }
}
s