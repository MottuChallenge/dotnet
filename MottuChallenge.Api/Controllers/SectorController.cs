using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Response;
using MottuChallenge.Application.UseCases.Sectors;

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
        public async Task<IActionResult> Post([FromBody] SectorCreateDto sectorCreateDto)
        {
            await _createSectorUseCase.SaveSector(sectorCreateDto);
            return Created();
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
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var sector = await _getSectorByIdUseCase.FindSectorById(id);
            return Ok(sector);
        }
    }
}
