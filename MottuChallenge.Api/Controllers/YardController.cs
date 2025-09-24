using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.DTOs.Validations;
using MottuChallenge.Application.Pagination;
using MottuChallenge.Application.UseCases.Yards;
using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/yards")]
    [ApiController]
    public class YardController : ControllerBase
    {
        private readonly CreateYardUseCase _createYardUseCase;
        private readonly GetAllYardsUseCase _getAllYardsUseCase;
        private readonly GetYardByIdUseCase _getYardByIdUseCase;
        private readonly UpdateYardUseCase _updateYardUseCase;
        private readonly DeleteYardUseCase _deleteYardUseCase;

        public YardController(
            CreateYardUseCase createYardUseCase,
            GetAllYardsUseCase getAllYardsUseCase,
            GetYardByIdUseCase getYardByIdUseCase,
            UpdateYardUseCase updateYardUseCase,
            DeleteYardUseCase deleteYardUseCase)
        {
            _createYardUseCase = createYardUseCase;
            _getAllYardsUseCase = getAllYardsUseCase;
            _getYardByIdUseCase = getYardByIdUseCase;
            _updateYardUseCase = updateYardUseCase;
            _deleteYardUseCase = deleteYardUseCase;
        }

        // POST: api/yards
        [HttpPost]
        [ProducesResponseType(typeof(YardResponseDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Post([FromBody] CreateYardDto createYardDto)
        {
            var validator = new CreateYardDtoValidator();
            var result = validator.Validate(createYardDto);

            if (!result.IsValid)
            {
                var modelState = new ModelStateDictionary();
                foreach (var failure in result.Errors)
                {
                    modelState.AddModelError(failure.PropertyName, failure.ErrorMessage);
                }

                return ValidationProblem(modelState);
            }

            try
            {
                var createdYard = await _createYardUseCase.SaveYard(createYardDto);
                // Retorna 201 Created com a URL do recurso criado (idealmente, incluindo o ID no header Location)
                return CreatedAtAction(nameof(GetById), new { id = createdYard.Id }, createdYard);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/yards
        [HttpGet]
        [ProducesResponseType(typeof(List<YardResponseDto>), 200)]
        public async Task<IActionResult> GetAllYardsAsync()
        {
            try
            {
                var yards = await _getAllYardsUseCase.FindAllYards();
                return Ok(yards);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/yards/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(YardResponseDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var yard = await _getYardByIdUseCase.FindYardById(id);
                return Ok(yard);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // PUT: api/yards/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateYardName([FromRoute] Guid id, [FromBody] UpdateYardDto dto)
        {
            try
            {
                await _updateYardUseCase.UpdateYardNameAsync(id, dto);
                return NoContent(); // Retorna 204 No Content para sucesso na atualização
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found caso o yard não seja encontrado
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request caso haja erro de validação
            }
        }

        // DELETE: api/yards/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteYard([FromRoute] Guid id)
        {
            try
            {
                await _deleteYardUseCase.DeleteYardAsync(id);
                return NoContent(); // Retorna 204 No Content em caso de sucesso na exclusão
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found caso o yard não seja encontrado
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request caso haja erro de validação
            }
        }

        // GET: api/yards/paginated
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedResult<YardResponseDto>), 200)]
        public async Task<IActionResult> GetAllPaginated(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? name = null,
            CancellationToken ct = default)
        {
            var pageRequest = new PageRequest
            {
                Page = page,
                PageSize = pageSize
            };

            var filter = new YardQuery
            {
                Name = name
            };

            try
            {
                var result = await _getAllYardsUseCase.FindAllYardPageable(pageRequest, filter, ct);
                return Ok(result); // Retorna 200 OK com os dados paginados
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
