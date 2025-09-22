using MottuChallenge.Application.Repositories;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class GetYardEntityByIdUseCase
    {
        private readonly IYardRepository _yardRepository;

        public GetYardEntityByIdUseCase(IYardRepository yardRepository)
        {
            _yardRepository = yardRepository;
        }

        public async Task<Yard> FindYardById(Guid id)
        {
            var yard = await _yardRepository.GetYardByIdAsync(id);
            ValidateYardExists(yard);

            return yard;
        }

        private void ValidateYardExists(Yard yard)
        {
            if (yard == null)
            {
                throw new KeyNotFoundException("Yard not Found");
            }
        }
    }
}
