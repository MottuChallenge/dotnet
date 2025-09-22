using MottuChallenge.Application.Repositories;
using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class DeleteYardUseCase
    {
        private readonly IYardRepository _yardRepository;

        public DeleteYardUseCase(IYardRepository yardRepository)
        {
            _yardRepository = yardRepository;
        }

        public async Task DeleteYardAsync(Guid yardId)
        {
            var yard = await _yardRepository.GetYardByIdAsync(yardId);
            if (yard == null)
                throw new KeyNotFoundException($"Yard with ID {yardId} not found.");

            await _yardRepository.DeleteAsync(yard);
        }
    }
}
