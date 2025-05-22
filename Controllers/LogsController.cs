// Controllers/LogsController.cs
using Microsoft.AspNetCore.Mvc;
using MottuGrid_Dotnet.Domain.Entities;
using MottuGrid_Dotnet.Domain.DTO.Request;
using MottuGrid_Dotnet.Domain.DTO.Response;
using MottuGrid_Dotnet.Infrastructure.Persistence.Repositories;

namespace MottuGrid_Dotnet.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    private readonly IRepository<Log> _logRepository;

    public LogsController(IRepository<Log> logRepository)
    {
        _logRepository = logRepository;
    }

    // GET: api/Logs
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<LogResponse>>> GetAll()
    {
        var logs = await _logRepository.GetAllAsync();
        return Ok(logs.Select(LogResponse.FromEntity));
    }

    // GET: api/Logs/{id}
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LogResponse>> GetById(Guid id)
    {
        var log = await _logRepository.GetByIdAsync(id);
        if (log == null) return NotFound();
        return Ok(LogResponse.FromEntity(log));
    }

    // POST: api/Logs
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<LogResponse>> Post(LogRequest request)
    {
        var log = new Log(request.Message, request.MotorcycleId);
        await _logRepository.AddAsync(log);
        var response = LogResponse.FromEntity(log);
        return CreatedAtAction(nameof(GetById), new { id = log.Id }, response);
    }

    // PUT: api/Logs/{id}
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(Guid id, LogRequest request)
    {
        var log = await _logRepository.GetByIdAsync(id);
        if (log == null) return NotFound();

        log.Update(request);
        await _logRepository.UpdateAsync(log);

        return NoContent();
    }

    // DELETE: api/Logs/{id}
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var log = await _logRepository.GetByIdAsync(id);
        if (log == null) return NotFound();

        await _logRepository.DeleteAsync(log);
        return NoContent();
    }
}
