using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.DTOs.Validations;
using MottuChallenge.Application.Pagination;
using MottuChallenge.Application.UseCases.Sectors;
using MottuChallenge.Domain.Exceptions;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/sectors")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly CreateSectorUseCase _createSectorUseCase;
        private readonly GetAllSectorsUseCase _getAllSectorsUseCase;
        private readonly GetSectorByIdUseCase _getSectorByIdUseCase;
        private readonly UpdateSectorUseCase _updateSectorUseCase;
        private readonly DeleteSectorUseCase _deleteSectorUseCase;

        public SectorController(
            CreateSectorUseCase createSectorUseCase,
            GetAllSectorsUseCase getAllSectorsUseCase,
            GetSectorByIdUseCase getSectorByIdUseCase,
            UpdateSectorUseCase updateSectorUseCase,
            DeleteSectorUseCase deleteSectorUseCase)
        {
            _createSectorUseCase = createSectorUseCase;
            _getAllSectorsUseCase = getAllSectorsUseCase;
            _getSectorByIdUseCase = getSectorByIdUseCase;
            _updateSectorUseCase = updateSectorUseCase;
            _deleteSectorUseCase = deleteSectorUseCase;
        }

        // POST: api/sectors
        [HttpPost]
        [ProducesResponseType(typeof(SectorResponseDto), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Post([FromBody] SectorCreateDto sectorCreateDto)
        {
            var validator = new SectorCreateDtoValidator();
            var result = validator.Validate(sectorCreateDto);

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
                var createdSector = await _createSectorUseCase.SaveSector(sectorCreateDto);

                // Retorna 201 Created com a resposta e URL do recurso criado (idealmente, incluindo o ID no header Location)
                return CreatedAtAction(nameof(GetById), new { id = createdSector.Id }, createdSector);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET: api/sectors
        [HttpGet]
        [ProducesResponseType(typeof(List<SectorResponseDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetAllSectorsAsync()
        {
            try
            {
                var sectors = await _getAllSectorsUseCase.FindAllSectors();
                return Ok(sectors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/sectors/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SectorResponseDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var sector = await _getSectorByIdUseCase.FindSectorById(id);
                return Ok(sector);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/sectors/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateSectorType([FromRoute] Guid id, [FromBody] UpdateSectorDto dto)
        {
            try
            {
                await _updateSectorUseCase.UpdateSectorTypeAsync(id, dto);
                return NoContent(); // Retorna 204 No Content para sucesso na atualização
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found caso o setor não seja encontrado
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request caso haja erro de validação
            }
        }

        // DELETE: api/sectors/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteSector([FromRoute] Guid id)
        {
            try
            {
                await _deleteSectorUseCase.DeleteSectorAsync(id);
                return NoContent(); // Retorna 204 No Content em caso de sucesso na exclusão
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found caso o setor não seja encontrado
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request caso haja erro de validação
            }
        }

        // GET: api/sectors/paginated
        [HttpGet("paginated")]
        [ProducesResponseType(typeof(PaginatedResult<SectorResponseDto>), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> GetAllPaginated(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] Guid? yardId = null,
            [FromQuery] Guid? sectorTypeId = null,
            CancellationToken ct = default)
        {
            try
            {
                var pageRequest = new PageRequest
                {
                    Page = page,
                    PageSize = pageSize
                };

                var filter = new SectorQuery
                {
                    YardId = yardId.GetValueOrDefault(Guid.Empty),
                    SectorTypeId = sectorTypeId.GetValueOrDefault(Guid.Empty)
                };

                var result = await _getAllSectorsUseCase.FindAllSectorPageable(pageRequest, filter, ct);
                return Ok(result); // Retorna 200 OK com os dados paginados
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
