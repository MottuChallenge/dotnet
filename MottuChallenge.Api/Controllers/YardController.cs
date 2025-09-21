using Microsoft.AspNetCore.Mvc;
using MottuChallenge.Application.DTOs.Request;
using MottuChallenge.Application.UseCases.Yards;

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
        public async Task<IActionResult> Post([FromBody] CreateYardDto createYardDto)
        {
            await _createYardUseCase.SaveYard(createYardDto);
            return Created();
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
        public async Task<IActionResult> getById([FromRoute] Guid id)
        {
            var yard = await _getYardByIdUseCase.FindYardById(id);
            return Ok(yard);
        }
       
    }
}
