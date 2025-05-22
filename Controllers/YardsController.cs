using Microsoft.AspNetCore.Mvc;
using MottuGrid_Dotnet.Domain.DTO.Request;
using MottuGrid_Dotnet.Domain.DTO.Response;
using MottuGrid_Dotnet.Domain.Entities;
using MottuGrid_Dotnet.Infrastructure.Persistence.Repositories;
using MottuGrid_Dotnet.Services;

namespace MottuGrid_Dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YardsController : ControllerBase
    {
        private readonly IRepository<Yard> _yardRepository;
        private readonly IRepository<Address> _addressRepository;
        private readonly CepService _cepService;

        public YardsController(IRepository<Yard> yardRepository, IRepository<Address> addressRepository, CepService cepService)
        {
            _yardRepository = yardRepository;
            _addressRepository = addressRepository;
            _cepService = cepService;
        }

        // GET: api/Yards
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<YardResponse>> GetYards()
        {
            var yards = await _yardRepository.GetAllAsync();

            var yardResponses = new List<YardResponse>();
            foreach (var yard in yards)
            {
                var address = await _addressRepository.GetByIdAsync(yard.AddressId);
                yard.Address = address;

                var addressResponse = new AddressResponse(address.Id, address.Street, address.Neighborhood, address.City, address.State, address.ZipCode, address.Country);
                var yardResponse = new YardResponse(yard.Id, yard.Name, yard.Area, addressResponse);

                yardResponses.Add(yardResponse);
            }

            return yardResponses;
        }

        // GET: api/Yards/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<YardResponse>> GetYard(Guid id)
        {
            var yard = await _yardRepository.GetByIdAsync(id);
            if (yard == null) return NotFound();

            var address = await _addressRepository.GetByIdAsync(yard.AddressId);
            if (address == null) return BadRequest();

            yard.Address = address;

            var addressResponse = new AddressResponse(address.Id, address.Street, address.Neighborhood, address.City, address.State, address.ZipCode, address.Country);
            var yardResponse = new YardResponse(yard.Id, yard.Name, yard.Area, addressResponse);

            return yardResponse;
        }

        // POST: api/Yards
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<YardResponse>> PostYard(YardRequest yardRequest)
        {
            var address = await _cepService.GetAddressByCep(yardRequest.Cep, yardRequest.Number);
            if (address == null) return BadRequest();

            await _addressRepository.AddAsync(address);

            var yard = new Yard(yardRequest.Name, yardRequest.Area, address.Id, yardRequest.BranchId);
            await _yardRepository.AddAsync(yard);

            var addressResponse = new AddressResponse(address.Id, address.Street, address.Neighborhood, address.City, address.State, address.ZipCode, address.Country);
            var yardResponse = new YardResponse(yard.Id, yard.Name, yard.Area, addressResponse);

            return CreatedAtAction(nameof(GetYard), new { id = yard.Id }, yardResponse);
        }

        // PUT: api/Yards/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutYard(Guid id, YardRequest yardRequest)
        {
            var yard = await _yardRepository.GetByIdAsync(id);
            if (yard == null) return NotFound();

            var newAddress = await _cepService.GetAddressByCep(yardRequest.Cep, yardRequest.Number);
            if (newAddress == null) return BadRequest();

            var existingAddress = await _addressRepository.GetByIdAsync(yard.AddressId);
            if (existingAddress == null) return BadRequest();

            existingAddress.UpdateAddress(newAddress);
            await _addressRepository.UpdateAsync(existingAddress);

            yard.UpdateYard(yardRequest);
            await _yardRepository.UpdateAsync(yard);

            return NoContent();
        }

        // DELETE: api/Yards/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteYard(Guid id)
        {
            var yard = await _yardRepository.GetByIdAsync(id);
            if (yard == null) return NotFound();

            await _yardRepository.DeleteAsync(yard);

            return NoContent();
        }
    }
}
