using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Pagination;
using MottuChallenge.Application.UseCases.Motorcycles;

namespace MottuChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly CreateMotorcycleUseCase _createMotorcycleUseCase;
        private readonly GetAllMotorcyclesPageableUseCase _getAllMotorcyclesPageableUseCase;

        public MotorcyclesController(
            CreateMotorcycleUseCase createMotorcycleUseCase,
            GetAllMotorcyclesPageableUseCase getAllMotorcyclesPageableUseCase)
        {
            _createMotorcycleUseCase = createMotorcycleUseCase;
            _getAllMotorcyclesPageableUseCase = getAllMotorcyclesPageableUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMotorcycle([FromBody] CreateMotorcycleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycle = await _createMotorcycleUseCase.SaveMotorcycleAsync(dto);

            return Ok(motorcycle);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllMotorcyclesPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pageRequest = new PageRequest();
            pageRequest.Page = page;
            pageRequest.PageSize = pageSize;

            var paginatedResult = await _getAllMotorcyclesPageableUseCase.FindAllMotorcyclePageable(pageRequest);

            // Map para DTOs
            var response = new PaginatedResult<MotorcycleResponseDto>(
                paginatedResult.Items.Select(m => new MotorcycleResponseDto
                {
                    Id = m.Id,
                    Model = m.Model,
                    EngineType = m.EngineType,
                    Plate = m.Plate,
                    LastRevisionDate = m.LastRevisionDate,
                    SpotId = m.SpotId
                }).ToList(),
                paginatedResult.TotalItems,
                paginatedResult.Page,
                paginatedResult.PageSize
            );

            return Ok(response);
        }
    }
}
