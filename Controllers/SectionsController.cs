using Microsoft.AspNetCore.Mvc;
using MottuGrid_Dotnet.Domain.DTO.Request;
using MottuGrid_Dotnet.Domain.Entities;
using MottuGrid_Dotnet.Infrastructure.Persistence.Repositories;

namespace MottuGrid_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionsController : ControllerBase
    {
        private readonly IRepository<Section> _sectorRepository;

        public SectionsController(IRepository<Section> sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        // GET: api/Sections
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Section>> GetSectors()
        {
            return await _sectorRepository.GetAllAsync();
        }

        // GET: api/Sections/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Section>> GetSector(Guid id)
        {
            var section = await _sectorRepository.GetByIdAsync(id);
            if (section == null) return NotFound();
            return section;
        }

        // POST: api/Sections
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Section>> PostSector(SectionRequest request)
        {
            var section = new Section(request.Color, request.Area, request.YardId);
            await _sectorRepository.AddAsync(section);

            return CreatedAtAction(nameof(GetSector), new { id = section.Id }, section);
        }

        // PUT: api/Sections/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutSector(Guid id, SectionRequest request)
        {
            var section = await _sectorRepository.GetByIdAsync(id);
            if (section == null) return NotFound();

            section.Update(request);
            _sectorRepository.UpdateAsync(section);

            return NoContent();
        }

        // DELETE: api/Sections/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSector(Guid id)
        {
            var sector = await _sectorRepository.GetByIdAsync(id);
            if (sector == null) return NotFound();

            _sectorRepository.DeleteAsync(sector);

            return NoContent();
        }
    }
}

