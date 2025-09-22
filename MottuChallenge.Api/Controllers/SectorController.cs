using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.DTOs.Validations;
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

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
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
                await _createSectorUseCase.SaveSector(sectorCreateDto);
                return Created();
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            } catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

       
        [HttpGet]
        [ProducesResponseType(typeof(List<SectorResponseDto>), 200)]
        public async Task<IActionResult> GetAllSectorsAsync()
        {
            var sectors = await _getAllSectorsUseCase.FindAllSectors();
            return Ok(sectors);
        }


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
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateSectorType([FromRoute] Guid id, [FromBody] UpdateSectorDto dto)
        {
            try
            {
                await _updateSectorUseCase.UpdateSectorTypeAsync(id, dto);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> DeleteSector([FromRoute] Guid id)
        {
            try
            {
                await _deleteSectorUseCase.DeleteSectorAsync(id);
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
