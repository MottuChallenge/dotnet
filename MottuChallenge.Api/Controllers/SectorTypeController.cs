using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Validations;
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
        private readonly GetSectorTypeByIdUseCase _getSectorTypeByIdUseCase;

        public SectorTypeController(
            CreateSectorTypeUseCase createSectorTypeUseCase,
            GetAllSectorTypesUseCase getAllSectorTypesUseCase,
            UpdateSectorTypeUseCase updateSectorTypeUseCase,
            DeleteSectorTypeUseCase deleteSectorTypeUseCase,
            GetSectorTypeByIdUseCase getSectorTypeByIdUseCase)
        {
            _createSectorTypeUseCase = createSectorTypeUseCase;
            _getAllSectorTypesUseCase = getAllSectorTypesUseCase;
            _updateSectorTypeUseCase = updateSectorTypeUseCase;
            _deleteSectorTypeUseCase = deleteSectorTypeUseCase;
            _getSectorTypeByIdUseCase = getSectorTypeByIdUseCase;
        }

        // POST: api/sectors_type
        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] SectorTypeDto sectorTypeCreateDto)
        {
            var validator = new SectorTypeDtoValidator();
            var result = validator.Validate(sectorTypeCreateDto);

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
                var createdSectorType = await _createSectorTypeUseCase.SaveSectorType(sectorTypeCreateDto);
                // Retorna 201 Created com a URL do recurso criado (idealmente, incluindo o ID no header Location)
                return CreatedAtAction(nameof(GetById), new { id = createdSectorType.Id }, createdSectorType);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/sectors_type
        [HttpGet]
        [ProducesResponseType(typeof(List<SectorTypeDto>), 200)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sectorTypes = await _getAllSectorTypesUseCase.FindAllSectorTypes();
                return Ok(sectorTypes); // Retorna 200 OK com a lista de tipos de setores
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/sectors_type/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] SectorTypeDto sectorTypeDto)
        {
            try
            {
                var updatedSectorType = await _updateSectorTypeUseCase.UpdateSectorTypeById(sectorTypeDto, id);
                return Ok(updatedSectorType); // Retorna 200 OK com o tipo de setor atualizado
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

        // DELETE: api/sectors_type/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                await _deleteSectorTypeUseCase.DeleteSectorTypeById(id);
                return NoContent(); // Retorna 204 No Content em caso de sucesso na exclusão
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // Retorna 404 Not Found caso o tipo de setor não seja encontrado
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message }); // Retorna 400 Bad Request em caso de falha de validação
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SectorTypeDto), 200)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var sectorType = await _getSectorTypeByIdUseCase.FindSectorTypeById(id);
                return Ok(sectorType);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
