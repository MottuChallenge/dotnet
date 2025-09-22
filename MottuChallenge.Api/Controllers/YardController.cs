using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.DTOs.Validations;
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

        public YardController(CreateYardUseCase createYardUseCase, GetAllYardsUseCase getAllYardsUseCase, GetYardByIdUseCase getYardByIdUseCase, UpdateYardUseCase updateYardUseCase)
        {
            _createYardUseCase = createYardUseCase;
            _getAllYardsUseCase = getAllYardsUseCase;
            _getYardByIdUseCase = getYardByIdUseCase;
            _updateYardUseCase = updateYardUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(string), 400)]
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
                await _createYardUseCase.SaveYard(createYardDto);
                return Created();
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(void), 200)]
        public async Task<IActionResult> GetAllYardsAsync()
        {
            var yards = await _getAllYardsUseCase.FindAllYards();
            return Ok(yards);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(void), 200)]
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

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(string), 400)]
        [ProducesResponseType(typeof(string), 404)]
        public async Task<IActionResult> UpdateYardName([FromRoute] Guid id, [FromBody] UpdateYardDto dto)
        {
            try
            {
                await _updateYardUseCase.UpdateYardNameAsync(id, dto);
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
