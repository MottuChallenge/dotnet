using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.Interfaces;
using MottuChallenge.Application.UseCases.Addresses;
using MottuChallenge.Domain.Entities;
using MottuChallenge.Domain.ValueObjects;
using MottuChallenge.Infrastructure.Repositories;

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

            var yard = new Yard(dto.Name, address.Id)
            {
                Address = address
            };

            foreach (var pointDto in dto.Points)
                yard.AddPoint(new PolygonPoint(pointDto.PointOrder, pointDto.X, pointDto.Y));

            await _yardRepository.SaveYardAsync(yard);
            return yard;
        }
    }
}
