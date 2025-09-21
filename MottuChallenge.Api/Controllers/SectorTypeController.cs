using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.SectorTypes;

namespace MottuChallenge.Api.Controllers
{
    [Route("api/sectors_type")]
    [ApiController]
    public class SectorTypeController : ControllerBase
    {
        private readonly CreateSectorTypeUseCase _createSectorTypeUseCase;
        private readonly GetAllSectorTypesUseCase _getAllSectorTypesUseCase;
        private readonly UpdateSectorTypeUseCase _updateSectorTypeUseCase;
        private readonly DeleteSectorTypeUseCase _deleteSectorTypeUseCase;

        public SectorTypeController(CreateSectorTypeUseCase createSectorTypeUseCase, GetAllSectorTypesUseCase getAllSectorTypesUseCase, UpdateSectorTypeUseCase updateSectorTypeUseCase, DeleteSectorTypeUseCase deleteSectorTypeUseCase)
        {
            _createSectorTypeUseCase = createSectorTypeUseCase;
            _getAllSectorTypesUseCase = getAllSectorTypesUseCase;
            _updateSectorTypeUseCase = updateSectorTypeUseCase;
            _deleteSectorTypeUseCase = deleteSectorTypeUseCase;
        }


        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            await _createSectorTypeUseCase.SaveSectorType(sectorTypeCreateDto);
            return Created();
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Get()
        {
            var sectors = await _getAllSectorTypesUseCase.FindAllSectorTypes();
            return Ok(sectors);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            var sectorType = await _updateSectorTypeUseCase.UpdateSectorTypeById(sectorTypeCreateDto, id);
            return Ok(sectorType);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _deleteSectorTypeUseCase.DeleteSectorTypeById(id);
            return NoContent();
        }
    }
}
