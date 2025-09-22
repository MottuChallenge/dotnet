using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Repositories;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;

namespace MottuChallenge.Application.UseCases.Yards
{
    public class CreateYardUseCase
    {
        private readonly IYardRepository _yardRepository;
        private readonly FindAddressByCepUseCase _findAddressByCepUseCase;

        public CreateYardUseCase(IYardRepository yardRepository, FindAddressByCepUseCase findAddressByCepUseCase)
        {
            _yardRepository = yardRepository;
            _findAddressByCepUseCase = findAddressByCepUseCase;
        }

        public async Task<Yard> SaveYard(CreateYardDto dto)
        {
            var address = await _findAddressByCepUseCase.FindAddressByCep(dto.Cep, dto.Number);

            var yard = new Yard(dto.Name);
            yard.SetAddress(address);

            foreach (var pointDto in dto.Points)
                yard.AddPoint(new PolygonPoint(pointDto.PointOrder, pointDto.X, pointDto.Y));

            await _yardRepository.SaveYardAsync(yard);
            return yard;
        }
    }
}
