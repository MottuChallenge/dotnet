using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
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

        public SectorController(CreateSectorUseCase createSectorUseCase, GetAllSectorsUseCase getAllSectorsUseCase, GetSectorByIdUseCase getSectorByIdUseCase)
        {
            _createSectorUseCase = createSectorUseCase;
            _getAllSectorsUseCase = getAllSectorsUseCase;
            _getSectorByIdUseCase = getSectorByIdUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Post([FromBody] SectorCreateDto sectorCreateDto)
        {
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
    }
}
