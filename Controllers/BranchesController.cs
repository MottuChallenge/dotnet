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
    public class BranchesController : ControllerBase
    {
        private readonly IRepository<Branch> _branchRepository;

        private readonly CepService _cepService;
        private readonly IRepository<Address> _addressRepository;

        public BranchesController(IRepository<Branch> branchRepository, CepService cepService, IRepository<Address> addressRepository)
        {
            _branchRepository = branchRepository;
            _cepService = cepService;
            _addressRepository = addressRepository;
        }

        // GET: api/Branches
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Branch>> GetBranches()
        {
            return await _branchRepository.GetAllAsync();
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Branch>> GetBranch(Guid id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        // POST: api/Branches
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Branch>> PostBranch(BranchRequest branchRequest)
        {
            var address = await _cepService.GetAddressByCep(branchRequest.Cep, branchRequest.Number);
            if(address == null)
            {
                return BadRequest();
            }
            var branch = new Branch(branchRequest.Name, branchRequest.CNPJ, address.Id, branchRequest.Phone, branchRequest.Email);
            await _addressRepository.AddAsync(address);
            await _branchRepository.AddAsync(branch);
            
            var addressResponse = new AddressResponse(address.Id, address.Street);

            return CreatedAtAction("GetBranch", new { id = branch.Id }, new BranchResponse(branch.Id, branch.Name, branch.CNPJ, branch.Phone, branch.Email, addressResponse, []));
        }

        // PUT: api/Branches/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutBranch(Guid id, Branch branch)
        {
            if (id != branch.Id)
            {
                return BadRequest();
            }

            _branchRepository.UpdateAsync(branch);

            return NoContent();
        }

        // DELETE: api/Branches/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBranch(Guid id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _branchRepository.DeleteAsync(branch);
            
            return NoContent();
        }
    }
}
