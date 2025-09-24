using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.Pagination;
using MottuChallenge.Application.UseCases.Motorcycles;
using MottuChallenge.Domain.Exceptions;
using System;
using System.Threading.Tasks;

namespace MottuChallenge.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotorcyclesController : ControllerBase
    {
        private readonly CreateMotorcycleUseCase _createMotorcycleUseCase;
        private readonly GetAllMotorcyclesPageableUseCase _getAllMotorcyclesPageableUseCase;
        private readonly UpdateMotorcycleUseCase _updateMotorcycleUseCase;
        private readonly DeleteMotorcycleUseCase _deleteMotorcycleUseCase;
        private readonly GetMotorcycleByIdUseCase _getMotorcycleByIdUseCase;

        public MotorcyclesController(
            CreateMotorcycleUseCase createMotorcycleUseCase,
            GetAllMotorcyclesPageableUseCase getAllMotorcyclesPageableUseCase,
            UpdateMotorcycleUseCase updateMotorcycleUseCase,
            DeleteMotorcycleUseCase deleteMotorcycleUseCase,
            GetMotorcycleByIdUseCase getMotorcycleByIdUseCase)
        {
            _createMotorcycleUseCase = createMotorcycleUseCase;
            _getAllMotorcyclesPageableUseCase = getAllMotorcyclesPageableUseCase;
            _updateMotorcycleUseCase = updateMotorcycleUseCase;
            _deleteMotorcycleUseCase = deleteMotorcycleUseCase;
            _getMotorcycleByIdUseCase = getMotorcycleByIdUseCase;
        }

        // POST: api/motorcycles
        [HttpPost]
        [ProducesResponseType(typeof(MotorcycleResponseDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> SaveMotorcycle([FromBody] MotorcycleDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var motorcycle = await _createMotorcycleUseCase.SaveMotorcycleAsync(dto);

                return CreatedAtAction(nameof(GetMotorcycleById), new { id = motorcycle.Id }, motorcycle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/motorcycles
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedResult<MotorcycleResponseDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetAllMotorcyclesPaginated([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] string? plate = null)
        {
            var pageRequest = new PageRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var filter = new MotorcycleQuery
            {
                Plate = plate
            };

            try
            {
                var paginatedResult = await _getAllMotorcyclesPageableUseCase.FindAllMotorcyclePageable(pageRequest, filter);

                return Ok(paginatedResult);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/motorcycles/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] MotorcycleDto dto)
        {
            try
            {
                var motorcycle = await _updateMotorcycleUseCase.UpdateMotorcycleAsync(id, dto);
                return NoContent(); // Retorna 204 No Content
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found se o recurso não for encontrado
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request para falha de validação
            }
        }

        // DELETE: api/motorcycles/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteMotorcycle([FromRoute] Guid id)
        {
            try
            {
                await _deleteMotorcycleUseCase.DeleteMotorcycleAsync(id);
                return NoContent(); // Retorna 204 No Content em caso de sucesso
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found caso a moto não seja encontrada
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request para falha de validação
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MotorcycleResponseDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetMotorcycleById([FromRoute] Guid id)
        {
            try
            {
                var motorcycle = await _getMotorcycleByIdUseCase.FindMotorcycleById(id);
                return Ok(motorcycle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

    }
}
