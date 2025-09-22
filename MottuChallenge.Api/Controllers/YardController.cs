using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
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

        public YardController(CreateYardUseCase createYardUseCase, GetAllYardsUseCase getAllYardsUseCase, GetYardByIdUseCase getYardByIdUseCase)
        {
            _createYardUseCase = createYardUseCase;
            _getAllYardsUseCase = getAllYardsUseCase;
            _getYardByIdUseCase = getYardByIdUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(void), 201)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> Post([FromBody] CreateYardDto createYardDto)
        {
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
       
    }
}
