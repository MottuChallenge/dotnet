using Microsoft.AspNetCore.Mvc;
using MottuGrid_Dotnet.Domain.Entities;
using MottuGrid_Dotnet.Domain.DTO.Request;
using MottuGrid_Dotnet.Domain.DTO.Response;
using MottuGrid_Dotnet.Infrastructure.Persistence.Repositories;

namespace MottuGrid_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcyclesController : ControllerBase
    {
        private readonly IRepository<Motorcycle> _motorcycleRepository;

        public MotorcyclesController(IRepository<Motorcycle> motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        // GET 1: Retorna todas as motos
        // GET: api/Motorcycles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MotorcycleResponse>>> GetAll()
        {
            var motorcycles = await _motorcycleRepository.GetAllAsync();
            var response = motorcycles.Select(MotorcycleResponse.FromEntity);
            return Ok(response);
        }

        // GET 2: Retorna moto por ID (PathParam)
        // GET: api/Motorcycles/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MotorcycleResponse>> GetById(Guid id)
        {
            var moto = await _motorcycleRepository.GetByIdAsync(id);
            if (moto == null) return NotFound();
            return Ok(MotorcycleResponse.FromEntity(moto));
        }

        // GET 3: Busca por modelo via QueryParam
        // GET: api/Motorcycles/search?model=Honda
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MotorcycleResponse>>> SearchByModel([FromQuery] string model)
        {
            var all = await _motorcycleRepository.GetAllAsync();
            var filtered = all.Where(m => m.Model.ToLower().Contains(model.ToLower()));
            return Ok(filtered.Select(MotorcycleResponse.FromEntity));
        }

        // GET 4: Lista motos por seção (PathParam)
        // GET: api/Motorcycles/by-section/{sectionId}
        [HttpGet("by-section/{sectionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MotorcycleResponse>>> GetBySection(Guid sectionId)
        {
            var all = await _motorcycleRepository.GetAllAsync();
            var filtered = all.Where(m => m.SectionId == sectionId);
            return Ok(filtered.Select(MotorcycleResponse.FromEntity));
        }

        // POST: api/Motorcycles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MotorcycleResponse>> Post(MotorcycleRequest request)
        {
            var moto = new Motorcycle(request.Model, request.EngineType, request.Plate, request.LastRevisionDate, request.SectionId);
            await _motorcycleRepository.AddAsync(moto);
            var response = MotorcycleResponse.FromEntity(moto);
            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        // PUT: api/Motorcycles/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, MotorcycleRequest request)
        {
            var moto = await _motorcycleRepository.GetByIdAsync(id);
            if (moto == null) return NotFound();
            moto.Update(request);
            _motorcycleRepository.UpdateAsync(moto);
            return NoContent();
        }

        // DELETE: api/Motorcycles/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var moto = await _motorcycleRepository.GetByIdAsync(id);
            if (moto == null) return NotFound();
            _motorcycleRepository.DeleteAsync(moto);
            return NoContent();
        }
    }
}
