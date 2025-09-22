using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.SectorTypes;
using MottuChallenge.Domain.Exceptions;

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
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            try
            {
                await _createSectorTypeUseCase.SaveSectorType(sectorTypeCreateDto);
                return Created();
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }      
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
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            try
            {
                var sectorType = await _updateSectorTypeUseCase.UpdateSectorTypeById(sectorTypeCreateDto, id);
                return Ok(sectorType);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           await _deleteSectorTypeUseCase.DeleteSectorTypeById(id);
           return NoContent();
        }
    }
}
