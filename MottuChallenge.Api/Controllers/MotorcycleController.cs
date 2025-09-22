using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Pagination;
using MottuChallenge.Application.UseCases.Motorcycles;
using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly CreateMotorcycleUseCase _createMotorcycleUseCase;
        private readonly GetAllMotorcyclesPageableUseCase _getAllMotorcyclesPageableUseCase;
        private readonly UpdateMotorcycleUseCase _updateMotorcycleUseCase;


        public MotorcyclesController(
            CreateMotorcycleUseCase createMotorcycleUseCase,
            GetAllMotorcyclesPageableUseCase getAllMotorcyclesPageableUseCase,
            UpdateMotorcycleUseCase updateMotorcycleUseCase)
        {
            _createMotorcycleUseCase = createMotorcycleUseCase;
            _getAllMotorcyclesPageableUseCase = getAllMotorcyclesPageableUseCase;
            _updateMotorcycleUseCase = updateMotorcycleUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> SaveMotorcycle([FromBody] MotorcycleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var motorcycle = await _createMotorcycleUseCase.SaveMotorcycleAsync(dto);

            return Ok(motorcycle);
        }


        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<MotorcycleResponseDto>), 200)]
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MotorcycleDto dto)
        {
            try
            {
                var motorcycle = await _updateMotorcycleUseCase.UpdateMotorcycleAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
